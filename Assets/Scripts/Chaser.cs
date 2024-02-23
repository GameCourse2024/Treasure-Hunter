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

    [SerializeField] private float stoppingDistance = 15f;

    [Header("For Display Only")]
    [SerializeField] private Vector3 playerPosition;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player != null)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        playerPosition = player.transform.position;
        float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);

        if (distanceToPlayer > stoppingDistance)
        {
            // Move towards the player
            navMeshAgent.SetDestination(playerPosition);
        }
        else
        {
            // Stop moving when within the stopping distance
            navMeshAgent.ResetPath();
        }

        FacePlayer();
    }

    private void FacePlayer()
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    internal void SetTarget(GameObject newTarget)
    {
        player = newTarget;
    }

    internal Vector3 TargetObjectPosition() {
        return player.transform.position;
    }
}





// using UnityEngine;
// using UnityEngine.AI;

// [RequireComponent(typeof(NavMeshAgent))]
// public class Chaser : MonoBehaviour
// {
//     [SerializeField] private GameObject player = null;
//      private NavMeshAgent navMeshAgent;
     
//     private bool isChaserMode;
//     private bool isAttacking; 
//     [Tooltip("The object that this enemy chases after")]
//     [SerializeField] private float stoppingDistance = 3f;

//     private void Start()
//     {
//         navMeshAgent = GetComponent<NavMeshAgent>();
//         isChaserMode = false;
//         isAttacking = false;
//     }

//     private void Update()
//     {
//         if (player != null)
//         {
//             if (isAttacking)
//             {
//                 FacePlayer();
//             }
//             else
//             {
//                 ChasePlayer();
//             }
//         }
//     }

//     private void ChasePlayer()
//     {
//         float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

//         if (distanceToPlayer > stoppingDistance)
//         {
//             // Move towards the player
//             navMeshAgent.SetDestination(player.transform.position);
//             isChaserMode = true;
//             isAttacking = false; // Not in attack mode while chasing
//         }
//         else
//         {
//             // Stop moving
//             navMeshAgent.ResetPath();
//             isChaserMode = false;

//             if (distanceToPlayer > resumeWanderDistance)
//             {
//                 // If out of resumeWanderDistance, reset player to null to resume wandering
//                 player = null;
//             }
//             else
//             {
//                 // Transition to attack mode when within attack range
//                 isAttacking = true;
//             }
//         }

//         if (isChaserMode)
//         {
//             FacePlayer();
//         }
//     }

//     private void FacePlayer()
//     {
//         Vector3 direction = (player.transform.position - transform.position).normalized;

//         // Check if the NPC is in attack mode before applying rotation
//         if (isAttacking)
//         {
//             Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
//             transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
//         }
//     }

//     internal void SetTarget(GameObject newTarget)
//     {
//         player = newTarget;
//     }

//     private bool IsInChaserMode()
//     {
//         return isChaserMode;
//     }
// }
