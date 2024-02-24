using UnityEngine;

[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(NPCAttack))]
public class EnemyControllerStateMachine : StateMachine
{
    [SerializeField] float radiusToWatch = 5f;
    [SerializeField] public float attackRange = 15f; 
    [SerializeField] float stoppingDistance = 10f; 
    [SerializeField] float probabilityToRotate = 0.2f;
    [SerializeField] float probabilityToStopRotating = 0.2f;

    private Chaser chaser;
    private Patroller patroller;
    private Rotator rotator;
    private NPCAttack attacking;

    private float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, chaser.TargetObjectPosition());
    }

    private void Awake()
    {
        chaser = GetComponent<Chaser>();
        patroller = GetComponent<Patroller>();
        rotator = GetComponent<Rotator>();
        attacking = GetComponent<NPCAttack>();

        base
            .AddState(patroller) 
            .AddState(chaser)
            .AddState(rotator)
            .AddState(attacking)
            .AddTransition(patroller, () => DistanceToTarget() <= radiusToWatch, chaser)
            .AddTransition(rotator, () => DistanceToTarget() <= radiusToWatch, chaser)
            .AddTransition(chaser, () => DistanceToTarget() > radiusToWatch, patroller)
            .AddTransition(rotator, () => Random.Range(0f, 1f) < probabilityToStopRotating * Time.deltaTime, patroller)
            .AddTransition(patroller, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, rotator)
            .AddTransition(chaser, () => DistanceToTarget() <= attackRange, attacking)
            .AddTransition(attacking, () => DistanceToTarget() > attackRange, chaser);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
