using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patroller : MonoBehaviour
{
    [Tooltip("Minimum time to wait at target between running to the next target")]
    [SerializeField] private float minWaitAtTarget = 7f;

    [Tooltip("Maximum time to wait at target between running to the next target")]
    [SerializeField] private float maxWaitAtTarget = 15f;

    [Tooltip("The distance the NPC moves left and right from the starting point")]
    [SerializeField] private float moveDistance = 10f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float rotationSpeed = 5f;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool movingRight = true;

    private float timeToWaitAtTarget;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        startPoint = transform.position;
        endPoint = startPoint + new Vector3(moveDistance, 0f, 0f);

        // Set the initial destination
        navMeshAgent.SetDestination(endPoint);
    }

    private void Update()
    {
        Patrol();
        UpdateAnimations();
        FaceDestination();
        animator.SetFloat("WalkingSpeed", navMeshAgent.velocity.magnitude);
    }

    private void Patrol()
    {
        // Check if the NPC has reached the destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            // Switch direction
            movingRight = !movingRight;

            // Set the new destination
            navMeshAgent.SetDestination(movingRight ? endPoint : startPoint);

            // Set random wait time
            //timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
        }

        // Reduce wait time
       // timeToWaitAtTarget -= Time.deltaTime;
    }

    private void UpdateAnimations()
    {
        // Agent is moving, play "Walk" animation
        animator.SetBool("isWalking", true);
    }

    private void FaceDestination()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
