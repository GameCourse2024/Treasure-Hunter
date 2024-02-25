using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float destroyTime = 1.5f;
    [SerializeField] private float shootingSpeed = 20f;

    public void SpawnFireball()
    {
        GameObject fireballInstance = Instantiate(fireballPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = fireballInstance.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * shootingSpeed;

        Destroy(fireballInstance, destroyTime);

    }
}


