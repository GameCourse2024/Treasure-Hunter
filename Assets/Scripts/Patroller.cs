using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patroller : MonoBehaviour
{
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float minWaitAtTarget = 7f;
    [SerializeField] private float maxWaitAtTarget = 15f;
    [SerializeField] private GameObject player; // Player object reference

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float rotationSpeed = 5f;
    private float timeToWaitAtTarget = 0;
    private Vector3 startPosition;

    private bool playerWasMoving = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;

        // Set the initial patrol target
        SelectRandomPatrolTarget();
    }

    private void SelectRandomPatrolTarget()
    {
        // Randomly select a point within the patrol radius
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += startPosition;

        // Ensure the point is on the NavMesh
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, patrolRadius, NavMesh.AllAreas);

        // Set the destination to the random patrol target
        navMeshAgent.SetDestination(navHit.position);

        // Set a random wait time at the target
        timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
    }

    private void Update()
    {
        if (navMeshAgent.hasPath)
        {
            FaceDestination();
            animator.SetBool("isWalking", true); // NPC is walking

            // Check if the player is moving
            bool playerIsMoving = CheckPlayerMovement();

            // Set isWalking based on player movement
            if (playerIsMoving)
            {
                playerWasMoving = true;
            }
            else if (playerWasMoving)
            {
                animator.SetBool("isWalking", false);
                playerWasMoving = false;
            }
        }
        else
        {
            animator.SetBool("isWalking", false); // NPC is not walking

            timeToWaitAtTarget -= Time.deltaTime;

            // If the waiting time is over, or the NPC reached the patrol target, select a new patrol target
            if (timeToWaitAtTarget <= 0 || Vector3.Distance(transform.position, navMeshAgent.destination) < 1f)
            {
                SelectRandomPatrolTarget();
            }

            // Check if the player is within the patrol radius
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null && Vector3.Distance(player.transform.position, transform.position) <= patrolRadius)
            {
                // Player is within the patrol radius, so set the destination to the player
                navMeshAgent.SetDestination(player.transform.position);
            }
        }
    }

    private void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private bool CheckPlayerMovement()
    {
        // You may need to replace this with your actual method of checking player movement
        // For simplicity, assuming the player is moving if their position changes significantly
        Vector3 playerPreviousPosition = player.transform.position;
        // Assuming a small threshold for movement detection
        float movementThreshold = 0.1f;

        // Move the player to trigger NavMeshAgent updates
        player.transform.Translate(Vector3.forward * Time.deltaTime);

        bool playerIsMoving = Vector3.Distance(playerPreviousPosition, player.transform.position) > movementThreshold;

        // Reset the player's position
        player.transform.position = playerPreviousPosition;

        return playerIsMoving;
    }
}
