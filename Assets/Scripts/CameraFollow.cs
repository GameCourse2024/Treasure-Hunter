using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Enter Ships Transform")]
    [SerializeField]
    private Transform target;  // Reference to the ship's transform

    [Tooltip("Desired X Camera Rotation")]
    [SerializeField]
    public float desiredXRotation = 30f;
    private float rotationSpeed = 5f;

    void Update()
    {
        if (target != null)
        {
            Quaternion targetRotation = Quaternion.Euler(desiredXRotation, target.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
