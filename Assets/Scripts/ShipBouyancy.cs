using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBouyancy : MonoBehaviour
{
    [Tooltip("How strong the ship bounces")]
    [SerializeField]
    private float bounceForce = 10f;

    [Tooltip("When we bounce")]
    [SerializeField]
    private float offsetY;


    private float startingY;

    private Rigidbody rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        startingY = transform.position.y;
    }

   private void Update() 
   {
        if(transform.position.y < startingY - offsetY)
        {
            BounceShip();
        }
   }

    void BounceShip()
    {
        // Apply an upward force to simulate the bounce
        Vector3 bounceForceVector = new Vector3(0f, bounceForce, 0f);
        rb.AddForce(bounceForceVector, ForceMode.Impulse);
    }

}
