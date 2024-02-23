using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    [Tooltip("How far this NPC will wander")]
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float minWanderTimer = 5f;
    [SerializeField] private float maxWanderTimer = 15f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private bool wander = true;
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 1000000f;

    private NavMeshAgent agent;
    private float timer;
    private Vector3 startPosition;
    private bool isInteracting;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = Random.Range(minWanderTimer, maxWanderTimer);
        startPosition = transform.position;
        isInteracting = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Random.Range(minWanderTimer, maxWanderTimer) && wander && !isInteracting)
        {
            //Debug.Log("Finding new position: " + name);
            HandleMovement();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the wander radius as a wire sphere around the NPC
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, NavMesh.AllAreas);

        return navHit.position;
    }

    private void HandleMovement()
    {
        //Debug.Log("Handling Movement: " + gameObject.name);
        Vector3 newPos = RandomNavSphere(startPosition, wanderRadius);
        agent.SetDestination(newPos);

        // Set walking animation and stop it upon reaching destination
        animator.SetBool("isWalking", true);
        StartCoroutine(MoveToDestination(newPos));

        timer = 0;
    }

    private IEnumerator MoveToDestination(Vector3 destination)
    {
        agent.SetDestination(destination);

        // Play walking animation
        animator.SetBool("isWalking", true);

        // Wait until the NPC reaches the destination
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        // Stop walking animation
        animator.SetBool("isWalking", false);

        // Wait for a short time before starting the next movement
        yield return new WaitForSeconds(waitTime);

        // Reset isInteracting to allow the NPC to move again
        isInteracting = false;
    }

    public void StopMovement()
    {
        // Stop the NavMeshAgent from moving
        //Debug.Log("Movement Stopped: " + name);
        agent.isStopped = true;
        isInteracting = true;

        // Rotating the NPC to the player
        StartCoroutine(RotateTowardsPlayer());
    }

    public void ResumeMovement()
    {
        // Resume the NavMeshAgent's movement
        //Debug.Log("Movement Un-Stopped: " + name);
        agent.isStopped = false;
        isInteracting = false;
    }

    private IEnumerator RotateTowardsPlayer()
    {
        //Debug.Log("Starting Rotation");
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        agent.isStopped = true;
        yield return new WaitForSeconds(waitTime);
    }
}