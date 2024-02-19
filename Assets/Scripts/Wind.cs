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

    [SerializeField]
    private float maxAngleChangePerSecond = 5f; // The maximum angle change per second
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
        targetAngle = Random.Range(-90f, 90f);
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
        targetAngle = Mathf.Clamp(targetAngle, -90f, 90f);

        // Smoothly lerp the current angle towards the target angle
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime);

        Debug.Log("Current Angle: " + currentAngle);

        windForceVector = Quaternion.Euler(0f, currentAngle, 0f) * Vector3.forward * windForce;
    }

    void ApplyWind()
    {
        // Calculate the wind force vector based on the current angle
        Vector3 windForceVector = Quaternion.Euler(0f, currentAngle, 0f) * Vector3.forward * windForce;
        // Apply the wind force to the boat
        rb.AddForce(windForceVector, ForceMode.Force);
    }
}