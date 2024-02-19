using UnityEngine;
using UnityEngine.UI;

public class WindCompass : MonoBehaviour
{
    public Wind windScript;
    public RectTransform compassNeedle;
    public float maxOffset = 10f; // Maximum offset to adjust the arrow's position
    public float rotationSpeed = 5f; // Adjust the rotation speed as needed

    private float previousWindAngle = 0f;

    void Start()
    {
        // Set the default wind direction if windScript is not assigned
        if (windScript == null)
        {
            Quaternion defaultRotation = Quaternion.Euler(0f, 0f, 90f);
            compassNeedle.rotation = defaultRotation;
        }
    }

    void Update()
    {
        if (windScript != null)
        {
            // Get the wind angle from the Wind script
            float windAngle = windScript.windAngle;

            // Invert the wind angle for leftward movement
            windAngle *= -1f;

            // Check if the wind direction has changed
            if (windAngle != previousWindAngle)
            {
                // Smoothly rotate the compass needle towards the wind angle
                compassNeedle.rotation = Quaternion.Euler(0f, 0f, windAngle);
            }
        }

        // Update the previous wind angle
        previousWindAngle = windScript.windAngle;
    }
}
