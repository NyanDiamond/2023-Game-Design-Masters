using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehavior : MonoBehaviour, IKickable, IPullable, IDamageable
{
    bool primed;
    private Moveable moveable;
    [SerializeField] GameObject explosion;
    private int health;
    [SerializeField] float fuse = 5f;
    // Start is called before the first frame update
    void Start()
    {
        health = 20;
        moveable = GetComponent<Moveable>();
        primed = false;
    }

    public void Kicked()
    {
        primed = true;
    }

    public void Pulled()
    {
    }

    public void Lassoed()
    {

    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Explode();
        }
        else
        {
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(fuse);
        Explode();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && moveable.isLaunched)
        {
            if (primed)
            {
                Explode();
            }
            ITrap trap = collision.gameObject.GetComponent<ITrap>();
            if(trap!=null)
            {
                trap.ActivateTrap(gameObject);
            }
            
            if (!collision.gameObject.CompareTag("Ground"))
            {
                TakeDamage(10);
                IDamageable dam = collision.gameObject.GetComponent<IDamageable>();
                if (dam!=null)
                    dam.TakeDamage(10);

            }
        }

    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
