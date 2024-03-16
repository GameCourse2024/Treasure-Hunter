using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Tooltip("How fast the ship moves forward")]
    [SerializeField]
    private float shipSpeed = 1f;

    [Tooltip("How fast the ship rotates")]
    [SerializeField]
    private float rotationSpeed = 5f;

    [Tooltip("How fast the ship accelerates")]
    [SerializeField]
    private float acceleration = 0.01f;

    [Tooltip("How fast the ship stops")]
    [SerializeField]
    private float stopForce = 0.5f;
   
    [Tooltip("Key for turning the ship left")]
    [SerializeField]
    private KeyCode leftKey = KeyCode.A;

    [Tooltip("Key for turning the ship right")]
    [SerializeField]
    private KeyCode rightKey = KeyCode.D;
   
   
    private Rigidbody rb;



    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        HandleMovementInput();    
    }

void HandleMovementInput()
{
    float rotationInput = 0f;

        if (Input.GetKey(leftKey))
        {
            rotationInput = -1f;
        }
        else if (Input.GetKey(rightKey))
        {
            rotationInput = 1f;
        }

        float rotationAmount = rotationInput * rotationSpeed;
        transform.rotation = Quaternion.Euler(0f, rotationAmount + transform.eulerAngles.y, 0f);
        Vector3 forwardDirection = transform.forward;
        Vector3 rightDirection = transform.right;

        // Calculate the force to stop the previous movement
        Vector3 oppositeForce = -rb.velocity * stopForce;

        oppositeForce.y = 0;
        rb.AddForce(oppositeForce, ForceMode.Impulse);
        rb.AddForce(forwardDirection * shipSpeed * acceleration * Time.deltaTime, ForceMode.Impulse);
    }
}

