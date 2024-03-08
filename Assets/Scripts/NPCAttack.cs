using UnityEngine;
using System.Collections;

public class NPCAttack : MonoBehaviour
{
    [Tooltip("The object NPC shoots")]
    [SerializeField] private GameObject projectilePrefab;
    [Tooltip("Where the projectile spawns from")]
    [SerializeField] private Transform projectileSpawnPoint;
    [Tooltip("Speed of projectile")]
    [SerializeField] private float projectileSpeed = 20f;
    [Tooltip("Cooldown between each shot")]
    [SerializeField] private float attackCooldown = 1.5f;
    [Tooltip("The damage the projectile provides")]
    [SerializeField] private float destroyTime = 1.5f;
    private Animator animator;  
    private DestroyOnTrigger destroyCode;
    private float timeSinceLastAttack;
    private Vector3 playerPosition;
    private Vector3 directionToPlayer;

    private void Start()
    {
        destroyCode = GetComponent<DestroyOnTrigger>();
        timeSinceLastAttack = attackCooldown;  // To allow the first attack immediately if desired
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //transform.rotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0f, directionToPlayer.z));
        timeSinceLastAttack += Time.deltaTime;
    
        if (!destroyCode.IsDead() && timeSinceLastAttack >= attackCooldown)
        {
            Attack();
            timeSinceLastAttack = 0f;
        }
    }

    public void Attack()
{
    if (projectilePrefab == null || projectileSpawnPoint == null)
    {
        Debug.LogError("Projectile prefab or spawn point is not assigned!");
        return;
    }

    // Get the direction to the player
    directionToPlayer = (GetPlayerPosition() - transform.position).normalized;

    // Calculate the rotation needed to face the player
    Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer, Vector3.up);

    // Rotate the projectile prefab 90 degrees around the Y-axis
    Quaternion arrowRotation = Quaternion.Euler(0f, 90f, 0f);

    // Apply the rotation to the projectile prefab
    Quaternion finalRotation = rotationToPlayer * arrowRotation;

    animator.SetBool("isAttacking", true);
    // Instantiate the projectile with the correct rotation
    GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, finalRotation);

    // Get the Rigidbody component of the projectile
    Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

    // Check if the projectile has a Rigidbody component
    if (projectileRigidbody != null)
    {
        // Set the velocity of the projectile to move towards the player
        projectileRigidbody.velocity = directionToPlayer * projectileSpeed;
    }
    else
    {
        Debug.LogError("Projectile prefab does not have a Rigidbody component!");
        Destroy(projectile); // Destroy the instantiated object if it doesn't have Rigidbody
    }

    // Destroy the projectile after a certain time
    Destroy(projectile, destroyTime);

    // Set the isAttacking parameter back to false after the attack animation
    StartCoroutine(ResetIsAttacking());
}



    private IEnumerator ResetIsAttacking()
    {
        // Wait for the attack animation duration (adjust this value based on your animation)
        yield return new WaitForSeconds(1f);

        // Reset the isAttacking parameter
        animator.SetBool("isAttacking", false);
    }

    private Vector3 GetPlayerPosition()
    {
        // You can replace this with your actual method of getting the player's position
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            return player.transform.position;
        }
        else
        {
            Debug.LogError("Player not found!");
            return Vector3.zero;
        }
    }
}
