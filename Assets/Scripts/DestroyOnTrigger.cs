using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class DestroyOnTrigger : MonoBehaviour
{
    public bool isDead = false;
    private Animator animator;
    [Tooltip("Tag of the triggering object that will destroy this object")]
    [SerializeField] private string triggeringTag;

    [Tooltip("Number of hits required to destroy the NPC")]
    [SerializeField] private int hitsRequired = 5;
    [Tooltip("Timer to display death animation")]
    [SerializeField] private float deathTimer = 7f;
    [Tooltip("Referrence to the animator prefab")]
    [SerializeField] private NavMeshAgent navMeshAgent; 

    private int count = 0;
    private bool isCooldown = false;
    private float cooldownDuration = 0.5f; // Adjust this value as needed

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>(); // Assign NavMeshAgent component

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the specified tag and the cooldown is not active
        if (other.CompareTag(triggeringTag) && !isCooldown)
        {
            Destroy(other.gameObject);

            // Increment the hit count
            count++;
            Debug.Log(count);

            // Start the cooldown
            StartCooldown();

            // Check if the hit count reaches the hitsRequired
            if (count >= hitsRequired)
            {   
                // Set the "isDead" parameter in the animator
                animator.SetBool("isDead", true);
                Debug.Log("isDead set to true");

                Destroy(other.gameObject);
                // Disable the NavMeshAgent to stop the NPC from moving
                if (navMeshAgent != null)
                    {
                        navMeshAgent.isStopped = true;
                        navMeshAgent.velocity = Vector3.zero;
                    }
                isDead = true;

                CancelInvoke("DestroyNPC");

                // Delay the destruction of the NPC
                Invoke("DestroyNPC", deathTimer);
            }
        }
    }

    private void StartCooldown()
    {
        // Set cooldown flag to true
        isCooldown = true;

        // Start a coroutine to reset cooldown flag after a specified duration
        StartCoroutine(ResetCooldown(cooldownDuration));
    }

    private IEnumerator ResetCooldown(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Reset cooldown flag
        isCooldown = false;
    }

    private void DestroyNPC()
    {
        // Destroy this object (NPC)
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
