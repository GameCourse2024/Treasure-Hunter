using UnityEngine;
using System.Collections;

public class DestroyOnTrigger : MonoBehaviour
{
    private Animator animator;
    [Tooltip("Tag of the triggering object that will destroy this object")]
    [SerializeField] private string triggeringTag;

    [Tooltip("Number of hits required to destroy the NPC")]
    [SerializeField] private int hitsRequired = 5;

    private int count = 0;
    private bool isCooldown = false;
    private float cooldownDuration = 1f; // Adjust this value as needed

    private void Start()
    {
        animator = GetComponent<Animator>();
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

                // Delay the destruction of the NPC
                Invoke("DestroyNPC", 4f);
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
}
