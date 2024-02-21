using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float staminaRegenRate = 10f; // Stamina regeneration per second
    [SerializeField] private float sprintingStaminaUsagePerSecond = 20f; // Stamina usage per second while sprinting
    private bool isSprinting = false;


    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthbar.SetMaxHealth(health);

        currentStamina = maxStamina;
        staminabar.SetMaxStamina(maxStamina);
        //staminabar.SetStamina(currentStamina);
        
        playerMovement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) TakeDamage(20);
        // TODO IF PLAYER GET HIT
        // NEED TO SET EACH DMG TO SPECIFIC ENEMY 
        // TakeDamage(damage);

        if (playerMovement.IsSprinting()) DealStamina();
        

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    void DealStamina()
    {
        if (playerMovement.IsSprinting() && currentStamina > 0)
        {
            isSprinting = true;
            currentStamina -= sprintingStaminaUsagePerSecond * Time.deltaTime;
            staminabar.SetStamina(currentStamina);
        }
        else
        {
            isSprinting = false;
            // Example: Stamina regeneration when not sprinting
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime; // Regen faster when not sprinting
                staminabar.SetStamina(currentStamina);
            }
            else if (currentStamina <= 0)
            {
                // If stamina is zero, wait for it to recharge to more than 10 before allowing sprinting
                isSprinting = false;
                //StartCoroutine(WaitForStaminaRecharge());
            }
        }
        
        playerMovement.SetSprinting(isSprinting);
    }
    IEnumerator WaitForStaminaRecharge()
    {
        // Wait until stamina is more than 10 before allowing sprinting again
        while (currentStamina <= 10)
        {
            yield return null;
        }

        isSprinting = true;
    }

}
