using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Josh Bonovich
//This abstract class holds all the necessary "interacton" behaviors. Such as if it gets hit or pulled
public abstract class EnemyInteractionBehaviorTemplate : MonoBehaviour, IPullable, IKickable
{
    [HideInInspector]
    protected bool lassoed;
    [HideInInspector]
    protected bool launched;
    [HideInInspector]
    protected bool hasCollided;
    [HideInInspector]
    public bool stunned;
    [HideInInspector]
    public EnemyBrain brain;
    [SerializeField] float breakOutTime = 5f;
    [SerializeField] Image lassoImage;
    [SerializeField] GameObject grabTimerUI;
    [SerializeField] Image grabTimerFill;

    [HideInInspector]
    public IsoAttackManager lassoOwner;
    [Tooltip("Stun time when taking damage")]
    [SerializeField] float staggerTime = 0.5f;
    protected bool coroutineRunning = false;


    public abstract void Kicked();
    public virtual void Lassoed()
    {
        lassoed = true;

        if (grabTimerFill != null) //(lassoImage != null)
        {
            //lassoImage.fillAmount = 0;
            //lassoImage.gameObject.SetActive(true);

            grabTimerFill.fillAmount = 1;
            grabTimerUI.SetActive(true);
        }
        StartCoroutine(BreakOut());
    }
    public virtual void Pulled(IsoAttackManager player = null)
    {
        //lassoImage.gameObject.SetActive(false);
    }
    public virtual void Break()
    {
        //if(lassoImage!= null)
        //lassoImage.gameObject.SetActive(false);
        if (grabTimerUI != null)
            grabTimerUI.SetActive(false);

        if (brain.moveable.tendrilOwner != null) brain.moveable.tendrilOwner.ForceRelease();
        else Debug.Log("LassoOwner = null");
    }
    public virtual void Stagger()
    {
        if (!stunned)
        {
            StopCoroutine(Staggered());
            StartCoroutine(Staggered());
        }
    }
    protected virtual void Stunned()
    {
        if(brain.state!=EnemyStates.DEAD)
            brain.state = EnemyStates.NOTHING;
        stunned = true;
    }

    public virtual void Stun(float time)
    {
        StopCoroutine(StunTimer(time));
        StartCoroutine(StunTimer(time));
    }

    public virtual void Death()
    {
        //Stunned();
        brain.health.Death();
    }
    protected virtual void UnStunned()
    {
        stunned = false;
        brain.PackAggro();
    }

    //the enemy gets itself out of a tendril
    protected IEnumerator BreakOut()
    {
        float timer = 0;
        while (lassoed)
        {
            timer += Time.deltaTime;
            //lassoImage.fillAmount = timer / breakOutTime;
            grabTimerFill.fillAmount = (1 - (timer / breakOutTime));
            if (timer >= breakOutTime) Break();
            yield return null;
        }
    }

    protected virtual IEnumerator StunTimer(float seconds)
    {
        coroutineRunning = true;
        Stunned();
        yield return new WaitForSeconds(seconds);
        UnStunned();
        coroutineRunning = false;
    }


    protected virtual IEnumerator Staggered()
    {
        if (!stunned)
        {
            Stunned();
            yield return new WaitForSeconds(staggerTime);
            UnStunned();
        }
        yield break;
    }

}
