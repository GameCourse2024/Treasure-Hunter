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
    [SerializeField] private int projectileDamage = 10;
    [Tooltip("Time that the projectile destroyed")]
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
        playerPosition = GetPlayerPosition();
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        // Get the direction to the player
        directionToPlayer = (GetPlayerPosition() - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0f, directionToPlayer.z));
        //if (distanceToPlayer <= stoppingDistance) animator.SetBool("isWalking", false);
    
        animator.SetBool("isAttacking", true);

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(directionToPlayer));
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        if (projectileRigidbody != null)
        {
            //Vector3 directionToPlayer = (GetPlayerPosition() - projectileSpawnPoint.position).normalized;
            projectileRigidbody.velocity = directionToPlayer * projectileSpeed;
            //projectileRigidbody.velocity = transform.forward * projectileSpeed;
            // Set the velocity to move straight towards the player with increased speed
            // projectileRigidbody.velocity = projectile.transform.forward * projectileSpeed;

            // // Disable gravity for the projectile
            // projectileRigidbody.useGravity = false;

            // Add rotation around the Y-axis gradually as it moves
            StartCoroutine(RotateArrow(projectile.transform));
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

    private IEnumerator RotateArrow(Transform arrowTransform)
    {
        while (arrowTransform != null)
        {
            // Rotate around the Y-axis gradually
            arrowTransform.Rotate(Vector3.up, 90f * Time.deltaTime);

            yield return null;
        }
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