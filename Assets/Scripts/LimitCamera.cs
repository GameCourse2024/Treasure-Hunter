using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCamera : MonoBehaviour
{
   [SerializeField] private GameObject player;
   [SerializeField] private float playerY = 28f;

   private void LateUpdate()
   {
    transform.position = new Vector3(player.transform.position.x, playerY, player.transform.position.z);
   
   }
}