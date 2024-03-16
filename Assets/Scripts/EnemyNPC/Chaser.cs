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
    private DestroyOnTrigger destroyCode;
    [Tooltip("The distance NPC should keep from target")]
    [SerializeField] private float stoppingDistance = 10f;
    [Tooltip("The speed of rotation")]
    [SerializeField] private float RotateSpeed = 5f;
    private Vector3 previousPosition;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        destroyCode = GetComponent<DestroyOnTrigger>();
        previousPosition = transform.position;    
    }

    private void Update()
    {
        animator.SetFloat("WalkingSpeed", navMeshAgent.velocity.magnitude);
        if (!destroyCode.IsDead())
        {
            ChasePlayer();
        }
        else
        {
            // Stop chasing when the NPC is dead
            navMeshAgent.isStopped = true;
            animator.SetBool("isWalking", false);
        }
    }

    private void ChasePlayer()
    {

        animator.SetBool("isWalking", true);
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            // Stop chasing when within stopping distance
            navMeshAgent.isStopped = true;
            animator.SetFloat("WalkingSpeed", navMeshAgent.velocity.magnitude);

            FacePlayer();           
        }
        else
        {
            // Resume chasing
            navMeshAgent.isStopped = false;
            animator.SetBool("isWalking", true);  // Optional: Set walking animation to true
            animator.SetFloat("WalkingSpeed", navMeshAgent.velocity.magnitude);

            FacePlayer();
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Vector3 targetPosition = player.transform.position - directionToPlayer * stoppingDistance;
            navMeshAgent.SetDestination(targetPosition);
        }
        animator.SetFloat("WalkingSpeed", navMeshAgent.velocity.magnitude);

    }

    private void FacePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotateSpeed);
    }

    internal Vector3 TargetObjectPosition()
    {
        return player.transform.position;
    }

}
