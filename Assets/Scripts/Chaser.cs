using System;
using UnityEngine;
using UnityEngine.AI;

/**
 * This component represents an enemy NPC that chases the player.
 */
[RequireComponent(typeof(NavMeshAgent))]
public class Chaser : MonoBehaviour
{
    [Tooltip("The object that this enemy chases after")]
    [SerializeField] GameObject player = null;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = 10f;
    }

    private void Update()
    {
        // playerPosition = player.transform.position;
        // float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        // FacePlayer();
        // navMeshAgent.SetDestination(playerPosition);
        ChasePlayer();
        
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= navMeshAgent.stoppingDistance)
        {
            // Stop chasing when within stopping distance
            navMeshAgent.isStopped = true;
        }
        else
        {
            // Resume chasing
            navMeshAgent.isStopped = false;
            FacePlayer();
            // Set the destination to the player position
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    internal Vector3 TargetObjectPosition()
    {
        return player.transform.position;
    }

    private void FaceDirection()
    {
        Vector3 direction = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
}
