using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItem : MonoBehaviour, IKickable, IPullable, IDamageable
{
    private Moveable moveable;
    [SerializeField] private int health = 20;
    [SerializeField] int clashDamage;
    [SerializeField] GameObject DestructionParticle;
    // Start is called before the first frame update
    void Start()
    {
        moveable = GetComponent<Moveable>();
    }

    public void Kicked()
    {

    }

    public void Pulled()
    {

    }

    public void Lassoed()
    {

    }

    public void Break()
    {

    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            //If there is a destruction particle, create it.
            if (DestructionParticle != null)
            {
                //create the particle
                GameObject vfxobj = Instantiate(DestructionParticle, gameObject.transform.position, Quaternion.identity);
                //destroy the particle
                Destroy(vfxobj, 4);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (moveable != null)
        {
            if (!collision.gameObject.CompareTag("Player") && moveable.isLaunched)
            {
                ITrap trap = collision.gameObject.GetComponent<ITrap>();
                if (trap != null)
                {
                    trap.ActivateTrap(gameObject);
                }

                if (!collision.gameObject.CompareTag("Ground"))
                {

                    IDamageable dam = collision.gameObject.GetComponent<IDamageable>();
                    if (dam != null)
                        dam.TakeDamage(clashDamage);
                    TakeDamage(clashDamage);
                }
            }
        }

    }

}
