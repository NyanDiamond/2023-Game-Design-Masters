using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(Animator))]
public class NewMeleeEnemyAttacks : EnemyAttackTemplate
{

    [Header("Dashing variables")]
    [SerializeField] float dashRange;
    [SerializeField] float dashTime;
    //[Header("JukeBox")]
    //[SerializeField] private JukeBox jukebox;

    [SerializeField] private EventReference enemyAggro;
    private void Awake()
    {
        //jukebox.SetTransform(transform);
    }
    public override void Attack()
    {
        if (count >= attackSpeed)
        {
            count = attackSpeed - 0.5f;
            AttackAI();
        }
    }

    private void AttackAI()
    {
        TriggerAttack(1);

    }

    private void TriggerAttack(int attack)
    {
        brain.an.SetFloat("AttackMod", 1);
        brain.an.SetBool("Attacking", true);
        //Debug.Log("trigger attack" + attack);
        currentWaitingTime = float.MaxValue;
        brain.state = EnemyStates.ATTACKING;
        brain.an.SetTrigger("Attack" + attack.ToString());
        brain.LookAtPlayer();
        //jukebox.PlaySound(0);
        AudioManager.instance.PlayOneShot(enemyAggro, this.transform.position);
        timeTest = Time.realtimeSinceStartup;
    }


    public void Dashing(int attack)
    {
        attack = attack - 1;
        if (attack < 0 && attack >= attackSeconds.Length)
        {
            Debug.LogError("Attack value for Dash invalid");
            return;
        }
        brain.moveable.Dash(transform.forward * dashRange, dashTime);
        currentWaitingTime = dashTime;
        if (animationTimer < 0)
            animationTimer = 0;
    }



    

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        animationTimer = float.MinValue;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCounter();
    }
}
