using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    private Animator animator;

    [Tooltip("Tag of the triggering object that will damage this object")]
    [SerializeField] string triggeringTag;

    [Tooltip("Amount of damage inflicted on each trigger collision")]
    [SerializeField] int damageAmount = 8;

    private PlayerStats stats;

    private bool isHitActive = false;
    private float hitDuration = 0.8f; // Adjust this value as needed
    private float hitTimer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        // Update the hit timer if it's active
        if (isHitActive)
        {
            hitTimer -= Time.deltaTime;

            // If the hit timer has elapsed, deactivate isHit
            if (hitTimer <= 0f)
            {
                isHitActive = false;
                animator.SetBool("isHit", false);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggeringTag) && !isHitActive)
        {
            Debug.Log(triggeringTag);
            
            // Inflict damage on the player
            TakeDamage(damageAmount);

            // Activate isHit and set the hit timer
            isHitActive = true;
            hitTimer = hitDuration;
            
            animator.SetBool("isHit", true);

            // Destroy the triggering object (fireball, NPC, etc.)
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        // Reduce player's health and update the health bar
        stats.HandleProjectileHit(damage);
    }
}
