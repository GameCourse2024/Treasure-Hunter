using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float destroyTime = 1.5f;
    [SerializeField] private float shootingSpeed = 20f;

    public void SpawnFireball()
    {
        // Get the camera's forward direction
        Vector3 shootingDirection = Camera.main.transform.forward;

        // Instantiate the fireball at the throwPoint's position
        GameObject fireballInstance = Instantiate(fireballPrefab, throwPoint.position, Quaternion.identity);

        // Get the fireball's rigidbody
        Rigidbody rb = fireballInstance.GetComponent<Rigidbody>();

        // Set the velocity based on the shooting direction and speed
        rb.velocity = shootingDirection * shootingSpeed;

        // Destroy the fireball after a certain time
        Destroy(fireballInstance, destroyTime);
    }
}
