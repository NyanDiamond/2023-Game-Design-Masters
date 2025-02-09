using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBombBehavior : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float stunTime;
    [SerializeField] LayerMask layerMask;
    List<GameObject> hit;
    ParticleSystem[] particleSystems;

    [Header("Sound")]
    [SerializeField] private JukeBox jukebox;

    private void Awake()
    {
        jukebox.SetTransform(transform);
    }
    private void Start()
    {
        jukebox.PlaySound(0);
        StartCoroutine(DestroySelf());
        hit = new List<GameObject>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        bool stillPlaying = false;
        foreach (ParticleSystem ps in particleSystems)
        {
            stillPlaying = ps.isPlaying;
        }
        if (!stillPlaying)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject entity = other.gameObject;
        bool visible = CanSee(other.transform);

        if (visible && !hit.Contains(entity))
        {
            hit.Add(entity);
            IDamageable damaged = entity.GetComponent<IDamageable>();
            if (damaged != null)
            {
                damaged.TakeDamage(dmg, DamageTypes.ENERGY);
            }
            EnemyInteractionBehaviorTemplate enemy = entity.GetComponent<EnemyInteractionBehaviorTemplate>();
            if (enemy != null) enemy.Stun(stunTime);

        }
    }

    private bool CanSee(Transform target)
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, target.position - transform.position, Color.red);
        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform == target)
            {
                return true;
            }
            else
            {
                //Debug.Log(hit.transform.name);
                return false;
            }
        }
        //Debug.Log("no hit");
        return false;
    }
}
