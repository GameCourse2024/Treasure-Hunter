using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShipCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has a tag
        if (collision.gameObject.tag == "Player")
        {
            // Print the tag of the object that hit the ship
            Debug.Log("Hit by: " + collision.gameObject.tag);
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

}
