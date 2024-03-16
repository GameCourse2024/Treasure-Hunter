using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float regenerationRate = 1.0f; 
    [SerializeField] private float regenerationInterval = 3.0f; 

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    private void Update()
    {
        if (currentHealth < maxHealth && currentHealth > 0)
        {
            currentHealth += regenerationRate * Time.deltaTime;
            slider.value = currentHealth;

            // Ensure health does not exceed the maximum value
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = Mathf.Max(0f, health);
        slider.maxValue = maxHealth;
        SetHealth(maxHealth);
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0f, maxHealth);
        slider.value = currentHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
