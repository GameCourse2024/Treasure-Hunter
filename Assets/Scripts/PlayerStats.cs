using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    public Healthbar healthbar;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public Staminabar staminabar;
    [SerializeField] private float maxStamina = 100f;
    private float currentStamina;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);

        currentStamina = maxStamina;
        staminabar.SetMaxStamina(currentStamina);
    }
    
    private void Update()
    {
        currentHealth = healthbar.GetHealth();
    }

    public void HandleProjectileHit(int damage)
    {
        TakeDamage(damage);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            string deathText = "Try Again!"; // Define deathText
            DeathManager.Instance.FadeOut(deathText);
        }
    }
}
