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
    private float hitDuration = 0.25f; // Adjust this value as needed
    private float hitTimer = 0f;

    [Tooltip("Name of sound effect for this character to play in interaction. NOTE list of sounds for npc can be found in the npc folder inside sounds")]
    [SerializeField] private string soundHit;

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
        if (other.CompareTag(triggeringTag)) // && !isHitActive)
        { 
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
        if(!stats.hasPlayedDeathSound) PlaySound();        // Reduce player's health and update the health bar
        stats.HandleProjectileHit(damage);
    }
    private void PlaySound()
    {
        //Debug.Log("Playing sound for NPC: " + soundName);
        AudioManagerGamePlay.Instance.Play(soundHit);
    }
}