using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateName : MonoBehaviour
{
    void Update()
    {
        Vector3 cameraDirection = Camera.main.transform.position - transform.position;
        cameraDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(cameraDirection, Vector3.up);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 180f, 0);
    }
}
