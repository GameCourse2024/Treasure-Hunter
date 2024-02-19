using UnityEngine;
using System.Collections;


public class Wind : MonoBehaviour
{
    public float windForce = 10f;
    public float maxWindAngleChange = 70f;
    public float rotationSpeed = 80f; // Increased value for faster rotation
    public float windChangeInterval = 5f; // Time interval for changing wind direction

    private Rigidbody shipRigidbody;
    private Vector3 targetWindDirection;

    public Vector3 windDirection { get; private set; }
    public float windAngle { get; private set; }

    void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();

        if (shipRigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on the GameObject with Wind script.");
        }

        // Initialize the target wind direction
        CalculateTargetWindDirection();

        // Start coroutine to periodically change wind direction
        StartCoroutine(ChangeWindDirection());
    }

    void Update()
    {
        ApplyWind();
    }

    void CalculateTargetWindDirection()
    {
        targetWindDirection = Random.onUnitSphere;
        targetWindDirection.y = 0f;
        targetWindDirection.Normalize();
    }

    IEnumerator ChangeWindDirection()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(windChangeInterval);

            // Calculate a new random wind direction
            CalculateTargetWindDirection();
        }
    }

    void ApplyWind()
{
    if (shipRigidbody != null)
    {
        // Get the current wind direction
        Vector3 currentWindDirection = shipRigidbody.velocity.normalized;

        // Calculate the angle between current and target wind directions
        float angle = Vector3.Angle(currentWindDirection, targetWindDirection);

        // Gradually rotate towards the target wind direction within the specified angle range
        float rotationAmount = Mathf.Min(maxWindAngleChange, angle);
        Vector3 newWindDirection = Vector3.RotateTowards(currentWindDirection, targetWindDirection, rotationAmount * Mathf.Deg2Rad, 0.0f);

        // Ignore changes in the Y-axis
        newWindDirection.y = 0f;

        // Gradually adjust the wind direction based on the previous direction
        targetWindDirection = Vector3.Slerp(currentWindDirection, newWindDirection, Time.deltaTime * rotationSpeed);
        Debug.Log(angle);
        Debug.Log(targetWindDirection);
        // Set the wind force directly to the ship's velocity (ignoring Y-axis)
        shipRigidbody.velocity = new Vector3(targetWindDirection.x, 0f, targetWindDirection.z) * windForce;

        // Set wind angle
        windAngle = Mathf.Atan2(targetWindDirection.x, targetWindDirection.z) * Mathf.Rad2Deg;

        // Continuously adjust the wind direction to simulate a smooth transition
        targetWindDirection = Quaternion.Euler(0f, Time.deltaTime * rotationSpeed, 0f) * targetWindDirection;
        targetWindDirection.Normalize(); // Normalize the direction to ensure constant speed
    }
}

}
