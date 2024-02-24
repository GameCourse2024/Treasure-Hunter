using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    [Tooltip("Tag of the triggering object that will damage this object")]
    [SerializeField] string triggeringTag;

    [Tooltip("Amount of damage inflicted on each trigger collision")]
    [SerializeField] int damageAmount = 8;

    private PlayerStats stats;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggeringTag))
        {
            Debug.Log(triggeringTag);
            // Inflict damage on the player
            TakeDamage(damageAmount);

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
