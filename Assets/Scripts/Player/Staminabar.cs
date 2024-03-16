using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float sprintingStaminaUsagePerSecond = 10f;
    [SerializeField] private float staminaRegenerationRate = 5f;
    [SerializeField] private float sprintCooldownTime = 5f; // Time to wait before starting stamina recharge after reaching 0

    private float currentStamina;
    private bool isSprinting = false;
    private float sprintCooldownTimer;

    public PlayerMovement movement;


    // New method to set max stamina externally
    public void SetMaxStamina(float stamina)
    {
        maxStamina = Mathf.Max(0f, stamina);
        slider.maxValue = maxStamina;
        SetStamina(maxStamina);  // Ensure currentStamina remains within the new maxStamina
    }

    public void SetStamina(float stamina)
    {
        currentStamina = Mathf.Clamp(stamina, 0f, maxStamina);
        slider.value = currentStamina;
    }

    private void Update()
    {
        if (movement.IsSprinting() && currentStamina > 0)
        {
            // Decrease stamina when sprinting
            DecreaseStamina(sprintingStaminaUsagePerSecond * Time.deltaTime);
        }
        else
        {
             // Start cooldown timer if stamina is at 0
            if (currentStamina <= 0)
            {
                StartSprintCooldown();
            }
            
            // Recharge stamina during cooldown
            if (sprintCooldownTimer > 0)
            {
                sprintCooldownTimer -= Time.deltaTime;
            }
            else
            {
                IncreaseStamina(staminaRegenerationRate * Time.deltaTime);
            }
        }
    }

    private void StartSprintCooldown()
    {
        // Start the cooldown timer when stamina reaches 0
        sprintCooldownTimer = sprintCooldownTime;
        isSprinting = false;
    }

    private void DecreaseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0f, maxStamina);
        SetStamina(currentStamina);
    }

    private void IncreaseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0f, maxStamina);
        SetStamina(currentStamina);
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }
}
