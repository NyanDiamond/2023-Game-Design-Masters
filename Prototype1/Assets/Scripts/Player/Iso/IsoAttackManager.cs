using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using FMODUnity;



public class IsoAttackManager : MonoBehaviour, ICanKick
{
    [Header("Lasso properties")]
    [SerializeField] [Tooltip("The speed of the lasso")] float lassoSpeed;
    [SerializeField] [Tooltip("The speed of the pull")] float minPullSpeed = 9f;
    [SerializeField] [Tooltip("The max speed of the pull")] float maxPullSpeed = 24f;
    [SerializeField] [Tooltip("Toss speed")] float tossSpeed = 9f;
    [SerializeField] [Tooltip("Time it takes to reach max speed")] float timeToMax = 1.5f;
    [SerializeField] [Tooltip("Time it takes holding to reset speed")] float timeToReset = 0.5f;
    float currentTime = 0;
    float restTime = 0;
    [SerializeField] [Range(0f, 1f)] [Tooltip("The % force of the pull if aiming fully away from the player")] float minPullForceModifier = 0.5f;
    [SerializeField] [Tooltip("How far a pulled object will go assuming 1 mass")] float pullCarryDistance;
    [SerializeField] [Tooltip("The time it takes for full charge")] float lassoChargeTime;
    [SerializeField] [Tooltip("Minimum distance throwing the lasso")] float minThrowLassoDistance;
    [SerializeField] [Tooltip("Maximum distance throwing the lasso")] float maxThrowLassoDistance;
    [SerializeField] [Tooltip("Maximum distance of lasso till it breaks")] float maxLassoDistance;
    [SerializeField] Slider lassoRangeUIIndicator;
    [SerializeField] GameObject lasso;
    [SerializeField] GameObject lassoOrigin;
    [SerializeField] float retractionSpeed;
    [SerializeField] GameObject tendril;
    [SerializeField] Image sliderFill;
    Rigidbody lassoRB;
    bool isRetracting;
    bool pulling;
    [HideInInspector] public LassoBehavior lb { get; private set; }
    //[SerializeField] [Tooltip("Toggle lasso pull mechanic")] bool toggleLasso;
    float currentLassoCharge;
    bool isCharging;
    bool releasedDuringCharge;
    LineRenderer lr;
    [Header("Kick Properties")]
    [SerializeField] [Tooltip("The force of the kick")] float kickForce;
    [SerializeField] [Tooltip("How far a kicked object will go assuming 1 mass")] float kickCarryDistance;
    [SerializeField] float kickCD;
    float trueKickCD = 0f;
    [SerializeField] Image kickCDIndicator;
    private bool canKick;
    MainControls mc;

    [Header("Charge mechanic")]
    [SerializeField]
    [Tooltip("Leave at 0 for infinite")]
    float chargeTime;
    [SerializeField]
    GameObject chargeDetonationPrefab;


    [Header("Sound")]
    //[SerializeField] private JukeBox jukebox;
    [SerializeField] private EventReference tendril1;
    [SerializeField] private EventReference tendril2;
    [SerializeField] private EventReference tendril3;
    [SerializeField] private EventReference tendril4;
    [SerializeField] private EventReference tendril5;
    [SerializeField] private EventReference tendril6;
    [SerializeField] private EventReference tendril7;
    [SerializeField] private EventReference tendril8;
    [SerializeField] private EventReference tendril9;
    [SerializeField] private EventReference tendrilsound;

    bool kicking;
    GameObject kick;
    [HideInInspector]
    public IsoPlayerController pc;
    GameController gc;
    Coroutine returnCall;

    [Header("Animator Variables")]
    [SerializeField] Animator anim; //assigned in inspector for now; can change
    [SerializeField] private Animator tendrilHandAnim;

    //[Header("TEMP Outlines")]
    //[SerializeField] OutlineToggle outlineToggle;
    private OutlineToggle outlineToggle;

    bool charged = false;
    int lastSelection;

    bool ignoreRelease = false;

    bool aiming;
    private void Awake()
    {
        //jukebox.SetTransform(transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        aiming = false;
        pulling = false;
        isRetracting = false;
        canKick = true;
        lr = GetComponent<LineRenderer>();
        isCharging = false;
        currentLassoCharge = 0;
        kicking = false;
        kick = transform.Find("KickHitbox").gameObject;
        kick.SetActive(false);
        pc = GetComponentInParent<IsoPlayerController>();
        gc = FindObjectOfType<GameController>();
        lb = lasso.GetComponentInChildren<LassoBehavior>();
        lasso.SetActive(false);
        lb.enabled = false;
        tendril.SetActive(false);
        lassoRB = lasso.GetComponent<Rigidbody>();
        lb.SetValues(maxThrowLassoDistance, maxLassoDistance, lassoRangeUIIndicator, sliderFill);
        outlineToggle = FindObjectOfType<OutlineToggle>();
        if (outlineToggle != null)
            outlineToggle.ToggleOutline(true);
        StartCoroutine(Aim());
    }

    private void OnEnable()
    {
        mc = ControlsContainer.instance.mainControls;
        mc.Main.Secondary.performed += Secondary;
        mc.Main.Secondary.canceled += Secondary;
        mc.Main.Primary.performed += Primary;
        mc.Main.Primary.canceled += Primary;
        //Note, naming on Release was before we moved release functionality to Secondary
        mc.Main.Release.performed += Release;
    }

    private void OnDisable()
    {
        mc.Main.Secondary.performed -= Secondary;
        mc.Main.Secondary.canceled -= Secondary;
        mc.Main.Primary.performed -= Primary;
        mc.Main.Primary.canceled -= Primary;
        mc.Main.Release.performed -= Release;
    }

    private void Primary(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            WindUp();
        if (ctx.canceled)
            Return();
    }

    private void Secondary(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            ToggleAim(true);
        if (ctx.canceled)
            ToggleAim(false);

    }

    private void Release(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            ForceRelease();
    }

    public (bool, GameObject, float duration) TakeCharge()
    {
        if (charged)
        {
            charged = false;
            return (true, chargeDetonationPrefab, chargeTime);
        }
        return (false, null, -1);
    }

    public void AquireCharge()
    {
        Debug.Log("Charge gained");
        charged = true;
        if (chargeTime > 0)
        {
            StopCoroutine("ChargeCountdown");
            StartCoroutine("ChargeCountdown");
        }
    }

    private IEnumerator ChargeCountdown()
    {
        yield return new WaitForSeconds(chargeTime);
        charged = false;
    }

    public void ActivateKick(GameObject target)
    {
        Debug.Log("Apply force");
        trueKickCD = kickCD;
        Moveable moveable = target.GetComponent<Moveable>();
        if (moveable != null)
            moveable.Launched(transform.forward * kickCarryDistance, kickForce);
        IKickable kick = target.GetComponent<IKickable>();
        if (kick != null)
            kick.Kicked();

    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0 && !pc.isStunned)
        {
            if (isRetracting)
            {
                lb.Retracting();
                pulling = false;
                if (lassoRB.isKinematic) { lassoRB.isKinematic = false; }
                lassoRB.velocity = (lassoOrigin.transform.position - lasso.transform.position).normalized * lassoSpeed;
                if (Vector3.Distance(lassoOrigin.transform.position, lasso.transform.position) <= lassoSpeed * Time.fixedDeltaTime)
                {
                    //Debug.Log(Time.fixedDeltaTime * lassoSpeed);
                    Retracted();
                    lassoRB.isKinematic = true;
                }
            }
            else
            {
                if (pulling)
                {
                    pc.attackState = Helpers.PULLING;
                    Pull(lb);
                }
            }

        }
    }

    #region NEO Controls
    private void WindUp()
    {
        if (Time.timeScale != 0 && !pc.isStunned && !pc.isDead && !kicking)
        {
            if (!pc.moveable.isLaunched)
            {
                if (!lasso.activeInHierarchy)
                {
                    if (!isCharging)
                    {
                        pc.attackState = Helpers.LASSOING;
                        pc.LookAtMouse(true);
                        releasedDuringCharge = false;
                        isCharging = true;
                        anim.SetTrigger("TendrilThrow");
                    }
                    else
                    {
                        releasedDuringCharge = false;
                    }
                    
                    //Comment out when you add the animation call
                    //Toss();
                }
            }
        }
    }

    public void Toss()
    {
        anim.ResetTrigger("NextState");
        isCharging = false;
        lasso.SetActive(true);
        tendril.SetActive(true);
        lb.enabled = true;
        lasso.transform.parent = null;
        lassoRB.isKinematic = false;
        lassoRB.velocity = transform.forward * lassoSpeed;
        //float currentDistance = minThrowLassoDistance + (maxThrowLassoDistance - minThrowLassoDistance) * currentLassoCharge / lassoChargeTime;
        lb.SetValues(maxThrowLassoDistance, maxLassoDistance, lassoRangeUIIndicator, sliderFill);
        lb.Launched();
        lasso.transform.forward = transform.forward;
        StartCoroutine(WaitUntilGrab());
        if(releasedDuringCharge)
        {
            Return();
        }
    }

    private IEnumerator WaitUntilGrab()
    {
        yield return new WaitUntil(() => isRetracting || lb.GetAttachment().Item1 != null);
        if (lb.GetAttachment().Item1 != null)
        {
            pulling = true;
            currentTime = 0;
            restTime = 0;
            InertiaBehavior ib = lb.GetAttachment().Item1.GetComponent<InertiaBehavior>();
            if (ib != null)
            {
                ib.massTimer = 0;
            }
        }
    }

    private void Return()
    {
        if (Time.timeScale != 0 && !pc.isStunned && !pc.isDead)
        {
            //Debug.Log("return");
            if (lasso.activeInHierarchy)
            {
                if (lb.GetAttachment().Item2 != null)
                {
                    bool flick = pulling;
                    Moveable moveable = lb.GetAttachment().Item2;
                    ForceRelease();
                    if (flick)
                    {
                        moveable.Tossed(tossSpeed);
                    }
                    //jukebox.PlaySound(Random.Range(1,4));
                    PickEffortSound(tendrilsound, Random.Range(1, 10));
                    //AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                }
                else
                {
                    StartCoroutine(WaitForRetraction());
                }
            }
            else if (isCharging)
            {
                releasedDuringCharge = true;
            }
        }
    }

    private void ToggleAim(bool toggle)
    {
        Debug.Log("Toggle aim: " + toggle);
        aiming = toggle;
    }

    private IEnumerator Aim()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (aiming && !lasso.activeInHierarchy)
            {
                var (success, position) = pc.GetMousePosition();
                if (success)
                {
                    var direction = position - transform.position;
                    direction = direction.normalized;
                    direction.y = 0;

                    lr.enabled = true;
                    currentLassoCharge = Mathf.Clamp(currentLassoCharge + Time.deltaTime, 0, lassoChargeTime);
                    float currentDistance = maxThrowLassoDistance;
                    Vector3[] positions = { transform.position, direction * currentDistance + transform.position };
                    lr.SetPositions(positions);
                }
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    #endregion



    public void PickEffortSound(EventReference tendrilsound, int selection)
    {
        switch (selection)
        {
            case 9:
                tendrilsound = tendril9;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 10));
                }
                break;
            case 8:
                tendrilsound = tendril8;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 10));
                }
                break;
            case 7:
                tendrilsound = tendril7;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 8));
                }
                break;
            case 6:
                tendrilsound = tendril6;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 8));
                }
                break;
            case 5:
                tendrilsound = tendril5;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 8));
                }
                break;
            case 4:
                tendrilsound = tendril4;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 8));
                }
                break;
            case 3:
                tendrilsound = tendril3;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 8));
                }
                break;
            case 2:
                tendrilsound = tendril2;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 8));
                }
                break;
            default:
                tendrilsound = tendril1;
                if (selection != lastSelection)
                {
                    AudioManager.instance.PlayOneShot(tendrilsound, this.transform.position);
                    lastSelection = selection;
                }
                else
                {
                    PickEffortSound(tendrilsound, Random.Range(1, 8));
                }
                break;
        }

        //lastSelection = selection;
    }

    public void ForceRelease()
    {
        if (Time.timeScale != 0)
        {
            if (lasso.activeInHierarchy && !isRetracting)
            {
                pc.attackState = Helpers.LASSOED;
                pulling = false;
                Retraction();
            }
        }
    }

    public void Release()
    {
        if (lasso.activeInHierarchy && returnCall == null)
        {
            //lassoRangeUIIndicator.gameObject.SetActive(false);
            returnCall = StartCoroutine(WaitForRetraction());
        }
    }

    private float CalculatePullSpeed()
    {
        return Mathf.Lerp(minPullSpeed, maxPullSpeed, currentTime / timeToMax);
    }

    public void Pull(LassoBehavior lb)
    {
        GameObject target;
        Moveable moveable;
        (target, moveable) = lb.GetAttachment();
        if (target != null)
        {
            if (moveable != null)
            {
                InertiaBehavior ib = target.GetComponent<InertiaBehavior>();
                Vector3 dir;
                float calculatedDistance;
                (dir, calculatedDistance) = lb.GetValues();
                if (dir != Vector3.zero)
                {
                    //Debug.Log(calculatedDistance);
                    //Debug.Log(dir.magnitude);
                    if (ib != null)
                    {
                        ib.CalculateMass();
                        ib.massTimer = Mathf.Clamp(ib.massTimer + Time.fixedDeltaTime, 0, ib.massDuration);
                    }
                    moveable.Launched(dir * calculatedDistance, CalculatePullSpeed());
                    Vector3 toPlayer = transform.position - moveable.transform.position;
                    moveable.transform.forward = toPlayer.normalized;
                    currentTime = Mathf.Clamp(currentTime + Time.fixedDeltaTime, 0, timeToMax);
                    restTime = 0;
                }
                else
                {
                    moveable.Hold();
                    if (restTime >= timeToReset)
                    {
                        currentTime = 0f;
                        if (ib != null)
                        {
                            ib.massTimer = 0;
                        }
                    }
                    else
                        restTime += Time.fixedDeltaTime;
                }

            }

            target.GetComponent<IPullable>().Pulled(this);
        }
        if (moveable == null)
        {
            ForceRelease();
        }
    }


    public void KickEnd()
    {
        if (lasso.activeInHierarchy)
            pc.attackState = Helpers.LASSOED;
        else
            pc.attackState = Helpers.NOTATTACKING;
        kick.SetActive(false);
        kicking = false;
        StartCoroutine(KickCD());
    }

    private IEnumerator KickCD()
    {

        for (float i = 0; i < trueKickCD; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            kickCDIndicator.fillAmount = i / trueKickCD;
        }
        canKick = true;
        trueKickCD = 0;
        kickCDIndicator.fillAmount = 1;
    }

    IEnumerator WaitForRetraction()
    {
        //yield return new WaitForSeconds(0.15f);
        yield return new WaitUntil(lb.TriggerRelease);
        Retraction();
        returnCall = null;
    }

    void Retraction()
    {
        if (!isRetracting)
        {
            isRetracting = true;
            pc.attackState = Helpers.LASSOED;
            //Debug.Log("Start retracting");
            lb.StartRetracting();
            //lassoRB.isKinematic = false;
            //lasso.transform.parent = null;
            tendrilHandAnim.SetTrigger("Retract");
        }
    }

    void Retracted()
    {
        isRetracting = false;
        lb.Retracted();
        lasso.transform.parent = transform;
        lasso.transform.localPosition = lassoOrigin.transform.localPosition;
        lasso.SetActive(false);
        tendril.SetActive(false);
        if (pc.attackState != Helpers.ATTACKING)
            pc.attackState = Helpers.NOTATTACKING;

        anim.SetTrigger("NextState");
        //if(outlineToggle != null)
        //outlineToggle.ToggleOutline(false);
    }


}
