using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public Healthbar healthbar;
    [SerializeField] private int health = 100;
    private int currentHealth;

    public Staminabar staminabar;
    [SerializeField] private float maxStamina = 100f;
    private float currentStamina;

    private void Start()
    {
        currentHealth = health;
        healthbar.SetMaxHealth(health);

        currentStamina = maxStamina;
        staminabar.SetMaxStamina(currentStamina);
    }

    private void HandlePlayerHit(int damage)
    {
        TakeDamage(damage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) TakeDamage(20);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
}
