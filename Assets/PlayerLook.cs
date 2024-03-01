using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2.0f;

    void Update()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Debug.Log($"Mouse Delta: {mouseDelta}");

        // Rotate the camera horizontally
        float horizontalRotation = mouseDelta.x * sensitivity;
        transform.Rotate(Vector3.up * horizontalRotation);

        // Rotate the camera vertically
        float verticalRotation = -mouseDelta.y * sensitivity;
        transform.Rotate(Vector3.right * verticalRotation);

        // Limit the vertical rotation (optional)
        float currentXRotation = transform.localEulerAngles.x;
        currentXRotation = (currentXRotation > 180) ? currentXRotation - 360 : currentXRotation;
        float desiredXRotation = Mathf.Clamp(currentXRotation, -80, 80);
        transform.localEulerAngles = new Vector3(desiredXRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
