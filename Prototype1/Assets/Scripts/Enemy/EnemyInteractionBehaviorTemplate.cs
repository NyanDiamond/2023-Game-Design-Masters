using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyInteractionBehaviorTemplate : MonoBehaviour, IPullable, IKickable
{
    [HideInInspector]
    protected bool lassoed;
    [HideInInspector]
    protected bool hasCollided;
    public bool stunned;
    [HideInInspector]
    public EnemyBrain brain;
    [SerializeField] float breakOutTime = 5f;
    [SerializeField] Image lassoImage; 
    
    

    public abstract void Kicked();
    public virtual void Lassoed()
    {
        lassoImage.fillAmount = 0;
        lassoImage.gameObject.SetActive(true);
        StartCoroutine(BreakOut());
    }
    public virtual void Pulled()
    {
        lassoImage.gameObject.SetActive(false);
    }
    public virtual void Break()
    {
        lassoImage.gameObject.SetActive(false);
        Destroy(gameObject.GetComponentInChildren<LassoBehavior>().gameObject);
    }
    public abstract void Stagger();
    protected abstract void Stunned();
    protected abstract void UnStunned();

    protected IEnumerator BreakOut()
    {
        float timer = 0;
        while(lassoed)
        {
            timer += Time.deltaTime;
            lassoImage.fillAmount = timer / breakOutTime;
            if (timer >= breakOutTime) Break();
            yield return null;
        }
    }

}
