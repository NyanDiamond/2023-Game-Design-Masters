using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

#if UNITY_EDITOR
using UnityEditor;
#endif

//Josh Bonovich
//The brain of all enemies that is the core for movement and attacking
public static class EnemyStates
{
    public const int NOTHING = 0;
    public const int ATTACKING = 1;
    public const int DEAD = 2;
    public const int CHARGING = 3;
    public const int ENRAGED = 4;
}

public class EnemyBrain : MonoBehaviour, IEnemy
{
    //The references to the other parts of the enemy
    [HideInInspector]
    public EnemyHealth health;
    [HideInInspector]
    public EnemyInteractionBehaviorTemplate interaction;
    protected EnemyMovement movement;
    protected EnemyAttackTemplate attack;
    [HideInInspector]
    public Moveable moveable;
    //player information
    //[HideInInspector]
    public Transform player;
    Vector3 lastKnownLocation;
    //detecting values
    [SerializeField] 
    [Tooltip("Range in which the enemy can see the player or any other enemy getting aggroed")]
    public float sightDistance;
    [HideInInspector]
    public bool isAggro;
    [HideInInspector]
    public Animator an;
    [SerializeField] 
    [Tooltip("What layers block LOS from the player")]
    LayerMask layermask;
    [Tooltip("READ ONLY")]
    public int state;
    [SerializeField] 
    [Tooltip("What distance does the creature want to stay in from the player")]
    public float optimalRange;

    [Space(5)]

    [Header("Animator Settings")]

    [Tooltip("Set the animator to another object?")]
    [SerializeField] bool useOutsideAnimator = false;
    [Tooltip("The outside animator to use.")]
    [SerializeField] GameObject outsideAnimator;

    [Space(5)]

    //[SerializeField] private JukeBox jukebox;

    [SerializeField] private EventReference enemyAmbient;
    [SerializeField] private EventReference enemyAggro;
    private void Awake()
    {
        //jukebox.SetTransform(transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        Starting();
    }

    protected virtual void Starting()
    {
        //If we set the player transform to something other than the player, don't reset this
        if (player == null) player = GameController.GetPlayer();
        //Debug.Log(player);
        isAggro = false;
        moveable = GetComponent<Moveable>();
        if (useOutsideAnimator && outsideAnimator != null) { an = outsideAnimator.GetComponent<Animator>(); }
        else { an = GetComponent<Animator>(); }
        an.logWarnings = false;
        health = GetComponent<EnemyHealth>();
        health.brain = this;
        interaction = GetComponent<EnemyInteractionBehaviorTemplate>();
        interaction.brain = this;
        movement = GetComponent<EnemyMovement>();
        movement.brain = this;
        attack = GetComponent<EnemyAttackTemplate>();
        if (attack != null)
            attack.brain = this;

        state = EnemyStates.NOTHING;
        StartCoroutine(Ambiance());
    }

    // Update is called once per frame
    void Update()
    {
        Updating();
    }

    protected virtual void Updating()
    {
        if (state != EnemyStates.DEAD)
        {
            //if not stunned and not attacking
            if (!interaction.stunned && state == EnemyStates.NOTHING)
            {
                CheckMovement();
                CheckRotation();
                CheckAttack();
                CheckArea();
            }
            //if stunned stop all movement calculations
            else if (interaction.stunned && moveable != null)
            {
                if (!moveable.isLaunched)

                    movement.Stop();
            }
        }
    }

    protected virtual void CheckMovement()
    {
        if (InRange(optimalRange))
        {
            movement.Move();
        }
        else
        {
            movement.Move();
        }
    }

    protected void CheckRotation()
    {
        if(state == EnemyStates.NOTHING)
        {
            if (CanSeePlayer() && isAggro)
            {
                Vector3 dir = (player.position - transform.position);
                dir.y = 0;
                Vector3 velocityNoY = dir.normalized;
                velocityNoY.y = 0;
                transform.forward = velocityNoY.normalized;
            }
            else if (movement.rb.velocity.x != 0 || movement.rb.velocity.z != 0)
                if (movement.isMoving)
                {
                    Vector3 velocityNoY = movement.rb.velocity.normalized;
                    velocityNoY.y = 0;
                    transform.forward = velocityNoY.normalized;
                }
        }
    }

    protected void CheckArea()
    {
        if (CanSeePlayer())
            PackAggro();
    }

    public void LookAtPlayer()
    {
        if (CanSeePlayer())
        {
            Vector3 dir = (player.position - transform.position);
            dir.y = 0;
            Vector3 velocityNoY = dir.normalized;
            velocityNoY.y = 0;
            transform.forward = velocityNoY.normalized;
        }
        else
        {
            Vector3 dir = (lastKnownLocation - transform.position).normalized;
            dir.y = 0;
            Vector3 velocityNoY = dir.normalized;
            velocityNoY.y = 0;
            transform.forward = velocityNoY.normalized;
        }
    }

    protected virtual void CheckAttack()
    {
        if(attack!=null)
            if (InRange(attack.maxAttackRange) && isAggro)
                attack.Attack();
    }

    public bool InRange(float distance)
    {
        return (Vector3.Distance(player.position, transform.position) < distance && CanSeePlayer());
    }


    private bool CanSeePlayer()
    {
        if (Vector3.Distance(player.position, transform.position) > sightDistance)
        {
            return false;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.position - transform.position, out hit, Mathf.Infinity, layermask))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                lastKnownLocation = hit.transform.position;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public bool CanSeePosition(Vector3 position)
    {
        if (Physics.Raycast(transform.position, position - transform.position, Vector3.Distance(transform.position, position), layermask))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CanSee(Transform target)
    {
        if(target == player)
        {
            return CanSeePlayer();
        }
        if (Vector3.Distance(target.position, transform.position) > sightDistance) return false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, Mathf.Infinity, layermask))
        {
            return hit.transform == target;
        }
        return false;
    }

    public bool CheckLOS(Vector3 posToCheck, Transform target)
    {
        RaycastHit hit;
        if(Physics.Raycast(posToCheck, target.position - posToCheck, out hit, Mathf.Infinity, layermask ))
        {
            return hit.transform == target;
        }
        return false;
    }

    public void PackAggro()
    {
        Aggro();
        foreach(EnemyBrain enemy in FindObjectsOfType<EnemyBrain>())
        {
            if(enemy.CanSee(transform))
            {
                enemy.Aggro();
            }
        }
    }
    public virtual void Aggro()
    {
        if (!isAggro)
        {
            isAggro = true;
            health.ec.AddAggro(gameObject);
            StopCoroutine(Ambiance());
            AudioManager.instance.PlayOneShotAttached(enemyAggro, this.gameObject);
            //If this enemy has an IsoCameraMover with the CameraOnAggro boolean, steal the camera
            if (GetComponent<IsoCameraMover>() && GetComponent<IsoCameraMover>().GetCamOnAggro())
            {
                GetComponent<IsoCameraMover>().StealCamera();
            }
        }
        
    }

    public void Deaggro()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator Ambiance()
    {
        while(!isAggro)
        {
            //jukebox.PlaySound(0);
            AudioManager.instance.PlayOneShotAttached(enemyAmbient, this.gameObject);
            yield return new WaitForSeconds(10f);
        }
    }

}

//Visualizer for changing the Enemy's sight.
//Tutorial followed: https://www.youtube.com/watch?v=ABuXRbJDdXs
//Sean did this. If this screws things up, blame me.
#if UNITY_EDITOR
[CustomEditor(typeof(EnemyBrain))]
public class EnemyBrainEditor : Editor
{
    public void OnSceneGUI()
    {
        //Link the EnemyMovement script into this editor class
        var linkedObject = target as EnemyBrain;

        //set the handle colors.
        Handles.color = Color.blue;

        //begin a check to see if we've changed anything.
        EditorGUI.BeginChangeCheck();

        //create a new float based on where we've dragged the radius sphere
        float newSightDistance = Handles.RadiusHandle(Quaternion.identity, linkedObject.transform.position, linkedObject.sightDistance, false);

        //check to see if the range has been changed
        if (EditorGUI.EndChangeCheck())
        {
            //if the range has been changed, we record that.
            Undo.RecordObject(target, "Update Range");
            //Now, we replace our wander radius with the new wander radius made by dragging the wander radius sphere. Yippeeeeee!!!!!!
            linkedObject.sightDistance = newSightDistance;
        }
    }
}
#endif