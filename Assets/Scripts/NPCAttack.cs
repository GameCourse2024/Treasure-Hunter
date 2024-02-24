using UnityEngine;
using System.Collections;

public class NPCAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private int projectileDamage = 10;
    [SerializeField] private float destroyTime = 1.5f;
    private Animator animator;  // Reference to the Animator component
    private DestroyOnTrigger destroyCode;
    private float timeSinceLastAttack;

    private void Start()
    {
        destroyCode = GetComponent<DestroyOnTrigger>();
        timeSinceLastAttack = attackCooldown;  // To allow the first attack immediately if desired
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
        Vector3 directionToPlayer = (GetPlayerPosition() - transform.position).normalized;

        // Check if the NPC is facing the player within a specified angle
        if (Vector3.Angle(transform.forward, directionToPlayer) <= 45f)
        {
            animator.SetBool("isAttacking", true);

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

            if (projectileRigidbody != null)
            {
                //Vector3 directionToPlayer = (GetPlayerPosition() - projectileSpawnPoint.position).normalized;
                projectileRigidbody.velocity = directionToPlayer * projectileSpeed;
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
    }

    private IEnumerator RotateArrow(Transform arrowTransform)
    {
        while (arrowTransform != null)
        {
            // Rotate around the Y-axis gradually
            arrowTransform.Rotate(Vector3.forward, 90f * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator ResetIsAttacking()
    {
        // Wait for the attack animation duration (adjust this value based on your animation)
        yield return new WaitForSeconds(0.3f);

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
