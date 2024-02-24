using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patroller : MonoBehaviour
{
    [SerializeField] private float minWaitAtTarget = 7f;
    [SerializeField] private float maxWaitAtTarget = 15f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float rotationSpeed = 5f;
    private float timeToWaitAtTarget = 0;
    //[SerializeField] private Transform player;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Assuming the player is tagged as "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player is tagged as 'Player'.");
            enabled = false; // Disable the script if the player is not found
            return;
        }

        SelectNewTarget(player.transform);
    }

    private void SelectNewTarget(Transform target)
    {
        Debug.Log("New player target: " + target.name);
        navMeshAgent.SetDestination(target.position);
        timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
    }

    private void Update()
    {
        if (navMeshAgent.hasPath)
        {
            FaceDestination();
            animator.SetBool("isWalking", true); // NPC is walking
        }
        else
        {
            animator.SetBool("isWalking", false); // NPC is not walking

            timeToWaitAtTarget -= Time.deltaTime;
            if (timeToWaitAtTarget <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    SelectNewTarget(player.transform);
                }
                else
                {
                    Debug.LogError("Player not found. Make sure the player is tagged as 'Player'.");
                    enabled = false;
                }
            }
        }
    }

    private void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
