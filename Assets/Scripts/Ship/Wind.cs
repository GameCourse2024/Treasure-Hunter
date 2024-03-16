using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField]
    private float windForce;

    [Tooltip("How often the wind hits, maximum")]
    [SerializeField]
    private float maxInterval = 5f;

    [Tooltip("Angle Range")]
    [SerializeField] private float angleRange = 90f;

    [SerializeField] private float maxAngleChangePerSecond = 5f; // The maximum angle change per second
    private float targetAngle; // The target wind angle
    private float currentAngle; // The current wind angle

   private Rigidbody rb;

    public Vector3 windForceVector { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.Log("Error, no rigidbody on ship found");
        }
        targetAngle = Random.Range(-angleRange, angleRange);
        currentAngle = targetAngle;
    }

    void Update()
    {
        UpdateWindDirection();
        ApplyWind();
    }

    void UpdateWindDirection()
    {
        // Gradually change the target angle
        targetAngle += Random.Range(-maxAngleChangePerSecond, maxAngleChangePerSecond) * Time.deltaTime;

        // Clamp the target angle to the range of -90 to 90 degrees
        targetAngle = Mathf.Clamp(targetAngle, -angleRange, angleRange);

        // Smoothly lerp the current angle towards the target angle
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime);

        // Calculate the wind force vector only with x and z components
        Vector3 tempWindForce = Quaternion.Euler(0f, currentAngle, 0f) * Vector3.forward * windForce;
        tempWindForce.y = 0f; // Set y component to 0 to cancel force along the y-axis

        // Assign the modified vector back to the property
        windForceVector = tempWindForce;
    }

    void ApplyWind()
    {
        // Calculate the wind force vector based on the current angle
        Vector3 windForceVector = Quaternion.Euler(0f, currentAngle, 0f) * Vector3.forward * windForce;
        // Apply the wind force to the boat
        rb.AddForce(windForceVector, ForceMode.Force);
    }
}