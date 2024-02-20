using UnityEngine;

public class ShipBuoyancy : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float depthBeforeSubmerged = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //originalDrag = rb.drag;
    }

    void FixedUpdate()
    {
        Buoyancy();
        ApplyWaterResistance();
    }

    void Buoyancy()
    {
        // Raycast downward to find the water surface
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Water")))
        {
            // Calculate the displacement from the water surface
            float displacement = Mathf.Max(0, hit.point.y - transform.position.y);

            // Calculate the buoyant force based on the displaced volume
            Vector3 buoyantForce = Physics.gravity * displacement * rb.mass;
            rb.AddForce(buoyantForce);
        }
    }

    void ApplyWaterResistance()
    {
        // Simulate water resistance or drag
        Vector3 waterResistance = -rb.velocity * 0.1f; // You may need to adjust this value
        rb.AddForce(waterResistance);

        // Simulate angular damping due to water
        rb.angularVelocity *= 0.99f; // You may need to adjust this value
    }
}
