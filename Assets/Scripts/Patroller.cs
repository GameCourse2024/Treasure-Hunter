using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Patroller : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;

    private Vector3 destPoint;
    private bool walkpointSet;
    [SerializeField] private float range;
    [SerializeField] private float rotationSpeed = 5f; // Adjust the rotation speed as needed
    [SerializeField] private float stuckThreshold = 0.1f; // Adjust the threshold for considering the agent stuck
    [SerializeField] private float stuckTimeThreshold = 3f; // Adjust the time threshold for considering the agent stuck
    private float timeStuck;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Patrol();
        UpdateAnimations();
        FaceDestination(); // Call the method to make the character face the destination
    }

    void Patrol()
    {
        if (!walkpointSet) SearchForDest();
        if (walkpointSet) agent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 10) walkpointSet = false;

        // Check if the agent is stuck
        if (agent.velocity.magnitude < stuckThreshold)
        {
            timeStuck += Time.deltaTime;
            Debug.Log("NPC STANDING IN PLACE" + timeStuck);
            if (timeStuck >= stuckTimeThreshold)
            {
                walkpointSet = false;
                animator.SetBool("isWalking", false);
            }
        }
        else timeStuck = 0f;
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down))
        {
            walkpointSet = true;
        }
    }

    void UpdateAnimations()
    {
        if (agent.velocity.magnitude > 1f)
        {
            // Agent is moving, play "Walk" animation
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Agent is not moving, play "Idle" animation
            animator.SetBool("isWalking", false);
        }
    }

    private void FaceDestination()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            Vector3 directionToDestination = (agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
