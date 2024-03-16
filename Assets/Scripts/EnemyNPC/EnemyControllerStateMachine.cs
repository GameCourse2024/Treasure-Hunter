using UnityEngine;

[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(NPCAttack))]
public class EnemyControllerStateMachine : StateMachine
{
    [Tooltip("If the target is within this NPC Radius he starts chasing.")]
    [SerializeField] float radiusToWatch = 5f;
    [Tooltip("The distance the NPC starts attacking the player.")]
    [SerializeField] public float attackRange = 15f;
    [Tooltip("The distance from the target where the NPC stops walking.")]
    [SerializeField] float stoppingDistance = 20f;

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
            .AddTransition(attacking, () => DistanceToTarget() > attackRange, chaser)
            .AddTransition(patroller, () => DistanceToTarget() <= stoppingDistance, attacking)
            .AddTransition(chaser, () => DistanceToTarget() <= stoppingDistance, attacking); // Transition from both patroller and chaser to attacking if within stopping distance
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
