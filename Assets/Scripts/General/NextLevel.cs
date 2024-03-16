using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has a tag
        if (collision.gameObject.tag == "Player")
        {
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        Debug.Log("Load Next Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
