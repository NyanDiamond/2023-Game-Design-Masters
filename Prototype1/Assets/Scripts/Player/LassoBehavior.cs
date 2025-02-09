using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using Unity.VisualScripting;

public class LassoBehavior : MonoBehaviour
{
    private GameController gc;
    public IsoAttackManager attackManager;
    //private Collider collider;
    private List<Collider> colliders;

    private float maxThrowDistance = 999;
    private float maxDistance = 999;
    private GameObject attached;
    private Rigidbody attachedRB;
    private RigidbodyConstraints prevConstraints;
    private GameObject attachedTendrilVisual;
    private bool grounded;
    private Vector3 startingPos;
    private Quaternion startingRot;
    private Vector3 forwardVector;
    private Vector3 rightVector;
    private Vector3 leftVector;
    //private LassoRange lassoRange;
    private float adjustedPullRange;
    private float maxPullDistance;
    private float minPullDistance;
    private float calculatedDistance;
    [SerializeField] [Tooltip("Set to 0 for old version, else this takes over")]float trajectoryArrowDistance = 0f;
    //[SerializeField] float pullAngle = 90f;
    private Transform player;
    private Vector3 dir;

    private Moveable moveable;
    private Camera cam;
    private LineRenderer lr;
    [SerializeField] private LayerMask groundMask;
    //[SerializeField] private JukeBox jukebox;
    bool thrown;

    [SerializeField] private EventReference tendrilUse;
    [SerializeField] private EventReference tendrilBreak;

    [SerializeField] private Animator tendrilHandAnim;

    // Start is called before the first frame update
    private void Awake()
    {
        //jukebox.SetTransform(transform);
        thrown = false;
        //collider = GetComponent<Collider>();
        attackManager = FindObjectOfType<IsoAttackManager>();
        //lassoRange = GetComponentInChildren<LassoRange>();
        grounded = false;
        attached = null;
        attachedRB = null;
        attachedTendrilVisual = null;
        prevConstraints = new RigidbodyConstraints();
        moveable = null;
        startingPos = transform.position;
        startingRot = transform.rotation;
        dir = Vector3.zero;
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        gc = FindObjectOfType<GameController>();
        player = attackManager.transform;
        //Handles.color = Color.cyan;
    }

    public void SetValues(float maxThrowRange, float breakRange, Slider slider, Image sliderFill)
    {
        if(lr ==null)
        {
            lr = GetComponent<LineRenderer>();
        }
        if (lr != null)
        {
            lr.enabled = false;
        }
        moveable = null;
        attached = null;
        startingPos = transform.position;
        this.maxPullDistance = 10;
        this.minPullDistance = maxPullDistance * 1;
        this.maxThrowDistance = maxThrowRange;
        //this.player = playerPos;
        this.maxDistance = breakRange;


    }

    public (Vector3, float) GetValues()
    {
        return (dir, calculatedDistance * attachedRB.mass);
    }


    public void Launched()
    {
        if(colliders == null)
            colliders = new List<Collider>(GetComponentsInChildren<Collider>(true));
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
            //Debug.Log("turned on lasso collider");
        }
        AudioManager.instance.PlayOneShot(tendrilUse, this.transform.position);
        //jukebox.PlaySound(0);
        thrown = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject temp = collision.gameObject;
        if (attached == null && !grounded)
        {
            IPullable pullable = null;
            if (temp.CompareTag("Ground") || temp.CompareTag("Wall"))
            {
                attackManager.Release();
            }
            else
            {
                pullable = temp.GetComponentInParent<IPullable>();
            }
            if (pullable!= null)
            {
                if (colliders == null)
                    colliders = new List<Collider>(GetComponentsInChildren<Collider>(true));
                foreach (Collider collider in colliders)
                    collider.enabled = false;
                if (pullable.GetType() == typeof(BossEnemyInteractions))
                {
                    attackManager.Release();
                    return;
                }
                
                attached = temp;
                forwardVector = (player.position - attached.transform.position).normalized;
                attachedTendrilVisual = temp.FindChildObjectWithTag("Tendriled");
                if(attachedTendrilVisual != null)
                {
                    attachedTendrilVisual.SetActive(true);
                    tendrilHandAnim.SetTrigger("Grab");
                }

                //Physics.IgnoreCollision(GetComponent<Collider>(), temp.GetComponent<Collider>(), true);
                //gameObject.transform.parent = temp.transform;
                //transform.localPosition = Vector3.zero;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                moveable = temp.GetComponentInParent<Moveable>();
                if (moveable != null)
                {
                    moveable.tendrilOwner = attackManager;
                    moveable.Grabbed();
                    attachedRB = temp.GetComponentInParent<Rigidbody>();
                    attached.GetComponentInParent<IPullable>().Lassoed();
                    prevConstraints = attachedRB.constraints;
                    temp.transform.up = Vector3.up;
                    Vector3 toPlayer = player.transform.position - temp.transform.position;
                    temp.transform.forward = toPlayer.normalized;
                    attachedRB.freezeRotation = true;
                    //lassoRange.SetAttached(attached.transform, attachedRB);
                    lr.enabled = true;
                    adjustedPullRange = maxPullDistance / attachedRB.mass;
                }
                else
                    attached.GetComponentInParent<IPullable>().Lassoed();
                if (gc.toggleLasso)
                {
                    dir = forwardVector;
                    attackManager.Pull(this);
                }
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        GameObject temp = other.gameObject;
        if(attached == null && !grounded)
        {
            if(temp.GetComponentInParent<IPullable>() != null)
            {
                attached = temp;
                attached.GetComponentInParent<IPullable>().Lassoed();
                Physics.IgnoreCollision(GetComponent<Collider>(), temp.GetComponent<Collider>(), true);
                gameObject.transform.parent = temp.transform;
                transform.localPosition = Vector3.zero;
                slider.value = 0;
                slider.gameObject.SetActive(true);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    } */

    private void FixedUpdate()
    {
        if (thrown)
        {
            if (Vector3.Distance(startingPos, transform.position) >= maxThrowDistance && attached == null) attackManager.Release();
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > maxDistance)
            {
                if (attached != null)
                    attached.GetComponentInParent<IPullable>().Break();
                attackManager.ForceRelease();
                if (moveable != null)
                    moveable.ForceRelease();
                moveable = null;
                attached = null;
                //jukebox.PlaySound(1);
                AudioManager.instance.PlayOneShot(tendrilBreak, this.transform.position);
            }

            if (moveable != null && !gc.toggleLasso)
            {

                CheckAngle();
                calculatedDistance = trajectoryArrowDistance == 0f ? maxPullDistance / attachedRB.mass : trajectoryArrowDistance;
                //(maxPullDistance - ((maxPullDistance - minPullDistance) / 180) * Mathf.Abs(angle)) / attachedRB.mass
                dir.y = 0;
                Vector3[] positions = { attached.transform.position, attached.transform.position + dir * calculatedDistance };
                //lassoRange.SetRangeArc(forwardVector, maxPullDistance, minPullDistance);
                lr.SetPositions(positions);
                //Debug.Log(calculatedDistance);
            }
            if (attached != null)
            {
                //Debug.Log(attached.transform.position);
                transform.position = attached.transform.position;
                if (!attached.activeInHierarchy || attached.IsDestroyed())
                {
                    attackManager.ForceRelease();
                }
            }
        }
    }

    public (bool success, Vector3 position) GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            //Debug.DrawRay(hitInfo.point, Vector3.down * 10, Color.red, 10f);
            return (success: true, position: hitInfo.point);

        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }

    private float CheckAngle()
    {
        forwardVector = (player.position - attached.transform.position).normalized;
        //Debug.DrawRay(attached.transform.position, forwardVector, Color.black);
        //Debug.DrawRay(attached.transform.position, rightVector, Color.green);
        //Debug.DrawRay(attached.transform.position, -rightVector, Color.red);
        if (InputChecker.instance.IsController())
        {
            var direction = Helpers.ToIso(attackManager.pc._aimInput);
            direction.y = 0;
            dir = direction.normalized;

        }
        else
        {
            bool check;
            Vector3 mouseVector;
            (check, mouseVector) = GetMousePosition();
            if (check)
            {
                Vector3 mouseAdjust = mouseVector;
                mouseAdjust.y = 0;
                Vector3 attachedAdjust = attached.transform.position;
                attachedAdjust.y = 0;
                if(Vector3.Distance(mouseAdjust, attachedAdjust) < 1)
                {
                    dir = Vector3.zero;
                    return 0;
                }
                var direction = mouseVector - attached.transform.position;
                direction.y = 0;
                dir = direction.normalized;
            }
            else return 0;
        }
        float angle = Vector3.Angle(forwardVector.normalized, dir);
        return angle;

    }



    public (GameObject, Moveable) GetAttachment()
    {
        return (attached, moveable);
    }

    public bool TriggerRelease()
    {
        bool returnValue = true;
        transform.parent = null;
        //lr.enabled = true;
        if (moveable != null && moveable.gameObject.activeInHierarchy)
        {
            //Debug.Log("movable avalible");
            returnValue = moveable.TriggerRelease();
            if (!returnValue)
                transform.position = moveable.transform.position;
            else
            {
                if(moveable != null)
                    moveable.tendrilOwner = null;
                moveable = null;
            }
                
                
        }
        //Debug.Log(returnValue);
        return returnValue;
    }

    public void Retracted()
    {
        lr.enabled = false;
        thrown = false;
    }

    public void Retracting()
    {
        if(attached!=null)
        {
            StartRetracting();
        }
    }

    public void StartRetracting()
    {
        if (colliders == null)
            colliders = new List<Collider>(GetComponentsInChildren<Collider>(true));
        foreach (Collider collider in colliders)
            collider.enabled = false;
        if (attached!=null)
        {
            attached.GetComponentInParent<IPullable>().Break();
            //jukebox.PlaySound(1);
        }
        if (attachedRB != null)
        {
            attachedRB.freezeRotation = false;
            attachedRB.constraints = prevConstraints;

            
        }
        if(attachedTendrilVisual != null)
        {
            attachedTendrilVisual.SetActive(false);
        }
        if (moveable!=null)
        {
            moveable.ForceRelease();
            moveable.tendrilOwner = null;
        }
        attachedRB = null;
        prevConstraints = default;
        attachedTendrilVisual = null;
        moveable = null;
        attached = null;
        lr.enabled = false;
        
    }


}
