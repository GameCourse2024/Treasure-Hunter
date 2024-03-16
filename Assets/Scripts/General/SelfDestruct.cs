using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float timeToDestruct = 15f;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(timeToDestruct);
        Destroy(gameObject);
    }
}