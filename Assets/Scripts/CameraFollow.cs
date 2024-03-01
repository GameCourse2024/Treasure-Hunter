using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float distance = 5f; // Distance from the player
    public float height = 3f; // Height above the player
    public float rotationSpeed = 2.0f; // Speed of camera rotation
    public float smoothSpeed = 0.125f; // Smoothness of camera movement
    [SerializeField] private float maxFlip = 70f;

    private float mouseX;
    private float mouseY;
    
    void LateUpdate()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform not assigned to CameraFollow script!");
            return;
        }

        // Rotate the camera based on mouse input
        RotateWithMouse();

        // Calculate the desired position for the camera
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Vector3 offset = rotation * Vector3.back * distance;
        Vector3 desiredPosition = playerTransform.position + Vector3.up * height + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the player
        transform.LookAt(playerTransform.position + Vector3.up * height);
    }

    private void RotateWithMouse()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Clamp vertical rotation to avoid flipping
        mouseY = Mathf.Clamp(mouseY, -maxFlip, maxFlip);
    }
}
