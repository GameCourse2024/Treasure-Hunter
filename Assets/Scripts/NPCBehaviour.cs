using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    [Tooltip("How far this NPC will wander")]
    [SerializeField]
    private float wanderRadius = 10f;

    [Tooltip("Minimum time before this NPC moves again")]
    [SerializeField]
    private float minWanderTimer = 5f;

    [Tooltip("Maximum time before this NPC moves again")]
    [SerializeField]
    private float maxWanderTimer = 15f;

    [Tooltip("Does this NPC Wander?")]
    [SerializeField]
    private bool wander = true;
    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private Vector3 startPosition;
    private bool isInteracting;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = Random.Range(minWanderTimer, maxWanderTimer);
        startPosition = transform.position;
        isInteracting = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Random.Range(minWanderTimer, maxWanderTimer) && wander && !isInteracting)
        {
            Debug.Log("Finding new position: " + name);
            HandleMovement();
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void HandleMovement()
    {
        Debug.Log("Handling Movement: " + gameObject.name);
        Vector3 newPos = RandomNavSphere(startPosition, wanderRadius, -1);
        agent.SetDestination(newPos);

        // TO DO
        // Set walking animation and stop it upon reaching destination


        timer = 0;
    }

    public void StopMovement()
    {
        // Stop the NavMeshAgent from moving
        Debug.Log("Movement Stopped: " + name);
        agent.isStopped = true;
        isInteracting = true;

        // TO DO
        // Stop walking animation and play Interact animation
    }

    public void ResumeMovement()
    {
        // Resume the NavMeshAgent's movement
        Debug.Log("Movement Un-Stopped: " + name);
        agent.isStopped = false;
        isInteracting = false;

        // TO DO
        // Stop interact animation
    }
}
