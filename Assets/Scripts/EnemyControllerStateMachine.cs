using UnityEngine;

[RequireComponent(typeof(NPCBehaviour))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(NPCAttack))]
public class EnemyControllerStateMachine : StateMachine
{
    [SerializeField] float radiusToWatch = 5f;
    [SerializeField] public float attackRange = 10f; // Add attack range
    [SerializeField] float probabilityToRotate = 0.2f;
    [SerializeField] float probabilityToStopRotating = 0.2f;

    private Chaser chaser;
    private NPCBehaviour patroller;
    private Rotator rotator;
    private NPCAttack attacking;

    private float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, chaser.TargetObjectPosition());
    }

    private void Awake()
    {
        chaser = GetComponent<Chaser>();
        patroller = GetComponent<NPCBehaviour>();
        rotator = GetComponent<Rotator>();
        attacking = GetComponent<NPCAttack>();

        base
            .AddState(patroller) // This would be the first active state.
            .AddState(chaser)
            .AddState(rotator)
            .AddState(attacking) // Added NPCAttack state
            .AddTransition(patroller, () => DistanceToTarget() <= radiusToWatch, chaser)
            .AddTransition(rotator, () => DistanceToTarget() <= radiusToWatch, chaser)
            .AddTransition(chaser, () => DistanceToTarget() <= attackRange, attacking)
            .AddTransition(attacking, () => DistanceToTarget() > attackRange, patroller) // Stop attacking when out of range
            .AddTransition(chaser, () => DistanceToTarget() > radiusToWatch, patroller)
            .AddTransition(rotator, () => Random.Range(0f, 1f) < probabilityToStopRotating * Time.deltaTime, patroller)
            .AddTransition(patroller, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, rotator);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
