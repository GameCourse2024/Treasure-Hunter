using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float destroyTime = 1.5f;
    [SerializeField] private float shootingSpeed = 20f;
    [SerializeField] private float maxShootingAngle = 35f;
    [SerializeField] private string soundShoot;

    public void SpawnFireball()
    {
        // Get the camera's forward direction
        Vector3 shootingDirection = Camera.main.transform.forward;

        // Get the player's forward direction
        Vector3 playerForward = transform.forward;

        // Calculate the angle between the camera's forward and player's forward
        float angle = Vector3.Angle(shootingDirection, playerForward);

        // Clamp the shooting angle to the maximum allowed angle
        float clampedAngle = Mathf.Clamp(angle, 0f, maxShootingAngle);

        // Calculate the clamped shooting direction
        Vector3 clampedShootingDirection = Quaternion.AngleAxis(clampedAngle, Vector3.Cross(playerForward, shootingDirection)) * playerForward;

        PlaySound();
        
        // Instantiate the fireball at the throwPoint's position
        GameObject fireballInstance = Instantiate(fireballPrefab, throwPoint.position, Quaternion.identity);

        // Get the fireball's rigidbody
        Rigidbody rb = fireballInstance.GetComponent<Rigidbody>();

        // Set the velocity based on the clamped shooting direction and speed
        rb.velocity = clampedShootingDirection * shootingSpeed;

        // Destroy the fireball after a certain time
        Destroy(fireballInstance, destroyTime);
    }

    private void PlaySound()
    {
        AudioManagerGamePlay.Instance.Play(soundShoot);

    }
}
