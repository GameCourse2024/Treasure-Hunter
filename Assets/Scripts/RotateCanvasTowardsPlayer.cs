using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanvasTowardsPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void Update()
    {
        if(player != null)
        {
            Vector3 playerDirection = player.position - transform.position;
            playerDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }
    }
}
