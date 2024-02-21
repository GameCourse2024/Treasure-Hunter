using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float sprintingStaminaUsagePerSecond = 10f;
    [SerializeField] private float staminaRegenerationRate = 5f;

    private PlayerMovement playerMovement;
    private float currentStamina;

    private void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Debug.Log("Initial Max Stamina: " + maxStamina);
        Debug.Log("Initial Current Stamina: " + currentStamina);
    }
    

    private void Update()
    {
        // Example: Regenerate stamina when not sprinting
        if (!playerMovement.IsSprinting() && currentStamina < maxStamina)
        {
            RegenerateStamina();
        }
    }

    public void DecreaseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaUI();
    }

    private void RegenerateStamina()
    {
        currentStamina += staminaRegenerationRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaUI();
    }

    private void UpdateStaminaUI()
    {
        float fillPercentage = currentStamina / maxStamina;
        slider.value = fillPercentage;
        Debug.Log("Current Stamina: " + currentStamina);
        Debug.Log("Max Stamina: " + maxStamina);
        Debug.Log("Fill Percentage: " + fillPercentage);
    }


    // New method to set stamina externally
    public void SetStamina(float value)
    {
        slider.value = value;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    // New method to set max stamina externally
    public void SetMaxStamina(float value)
    {
        slider.maxValue = maxStamina;
        slider.value = currentStamina;
    }
}
