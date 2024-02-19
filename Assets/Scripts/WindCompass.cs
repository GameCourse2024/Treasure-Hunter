using UnityEngine;
using UnityEngine.UI;

public class WindCompass : MonoBehaviour
{
    [SerializeField]
    private Transform shipTransform; 

    [SerializeField]
    private Wind windScript;
    [SerializeField]
    private RectTransform arrowTransform;
    [SerializeField]
    private float rotationSpeed = 5f; // Speed at which the arrow rotates

     void Update()
    {
        if (windScript != null && arrowTransform != null && shipTransform != null)
        {
            // Calculate the angle between the forward direction and the wind direction
            float angle = Mathf.Atan2(windScript.windForceVector.x, windScript.windForceVector.z) * Mathf.Rad2Deg;

            // Calculate the relative angle based on the ship's rotation
            float relativeAngle = angle - shipTransform.eulerAngles.y;

            // Rotate the arrow to point towards the wind direction relative to the ship
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, -relativeAngle);
            arrowTransform.rotation = Quaternion.Slerp(arrowTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}