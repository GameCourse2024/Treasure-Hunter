using UnityEngine;

[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(NPCAttack))]
public class EnemyControllerStateMachine : StateMachine
{
    [SerializeField] float radiusToWatch = 5f;
    [SerializeField] public float attackRange = 15f; 
    private Chaser chaser;
    private Patroller patroller;
    private NPCAttack attacking;

    private float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, chaser.TargetObjectPosition());
    }

    private void Awake()
    {
        chaser = GetComponent<Chaser>();
        patroller = GetComponent<Patroller>();
        attacking = GetComponent<NPCAttack>();

        base
            .AddState(patroller) 
            .AddState(chaser)
            .AddState(attacking)
            .AddTransition(patroller, () => DistanceToTarget() <= radiusToWatch, chaser)
            .AddTransition(chaser, () => DistanceToTarget() > radiusToWatch, patroller)
            .AddTransition(chaser, () => DistanceToTarget() <= attackRange, attacking)
            .AddTransition(attacking, () => DistanceToTarget() > attackRange, chaser);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
