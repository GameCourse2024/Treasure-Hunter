using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    [Tooltip("What to call this point on the banner")]
    [SerializeField]
    private string pointName;
    [Tooltip("How long should the banner wait?")]
    [SerializeField]
    private float bannerWaitTime;
    [Tooltip("Radius around the point of interest")]
    [SerializeField]
    private float radius = 3f;

    private SphereCollider sphereCollider;

    private void Start()
    {
        // Get the SphereCollider component attached to the GameObject
        sphereCollider = GetComponent<SphereCollider>();

        // Set the collider's radius based on the script's radius value
        sphereCollider.radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the radius of the point of interest");
            // Perform any actions you want when the player enters the radius
            ScrollController.DisplayBanner(pointName,bannerWaitTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the radius in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}