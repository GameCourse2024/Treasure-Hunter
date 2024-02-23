using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDeathScript : MonoBehaviour
{
    [SerializeField] 
    private Transform player;
    [SerializeField] 
    private Vector2 xBounds;
    [SerializeField] 
    private Vector2 yBounds;
    [SerializeField] 
    private Vector2 zBounds;
    [Tooltip("Text to print to screen")]
    [SerializeField]
    private string deathText;



    private void Update()
    {
         if (player.position.x < xBounds.x || player.position.x > xBounds.y ||
            player.position.y < yBounds.x || player.position.y > yBounds.y ||
            player.position.z < zBounds.x || player.position.z > zBounds.y)
            {
                Debug.Log("Left Borders, time to die");
                DeathManager.Instance.FadeOut(deathText);
            }
    }
}
