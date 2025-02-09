using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CombatMissionBehavior : MissionBehavior
{
    [SerializeField] List<EnemyBrain> enemies;
    int startingCount;
    [SerializeField] List<GameObject> arenaBarriers;
    //bool completed;

    [SerializeField] bool disableBarriers = true;
    [SerializeField] bool isNextWave = false;
    //[SerializeField] new protected EventReference objectiveSound;

    //public GameObject AManager;
    //public GameObject musicTrigger;
    private void Awake()
    {
        //if (AManager == null) AManager = FindObjectOfType<AudioManager>().gameObject;
        startingCount = enemies.Count;
        completed = false;

    }

    private void OnEnable()
    {
        if (isNextWave)
            StartCombat();
    }
    protected override void OnTriggered()
    {
        StartCombat();
    }

    void StartCombat()
    {
        foreach (EnemyBrain enemy in enemies)
        {
            enemy.Aggro();
        }
        foreach (GameObject barrier in arenaBarriers)
        {
            barrier.SetActive(true);
        }
        folder.StartCombat(this);
        RemoveEnemy(null);
    }

    public string GetCount()
    {
        string message = startingCount - enemies.Count + "/" + startingCount;
        return message;
    }

    public void RemoveEnemy(EnemyBrain enemy)
    {
        Debug.Log("enemy killed");
        if(enemies.Contains(enemy) && !completed)
        {
            enemies.Remove(enemy);
            Debug.Log("enemy count: " + enemies.Count);
            if (enemies.Count <= 0)
                OnComplete();
        }
    }

    public override void OnComplete()
    {
        Debug.Log("Completed");
        //completed = true;
        if(enemies.Count>0)
        {
            foreach(EnemyBrain enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        enemies.Clear();

        foreach (GameObject barrier in arenaBarriers)
        {
            if (barrier.GetComponent<BarrierController>() != null && disableBarriers)
                barrier.GetComponent<BarrierController>().ToggleBarrier(false);
            else if (disableBarriers)
                barrier.SetActive(false);
        }

        folder.CombatFinished();
        //Destroy(musicTrigger);
        //AManager.GetComponent<FMODUnity.StudioEventEmitter>().Play();
        AudioManager.instance.PlayOneShot(objectiveSound, this.transform.position);
        base.OnComplete();
    }
}
