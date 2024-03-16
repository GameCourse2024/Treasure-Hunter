using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickForNextLevel : MonoBehaviour
{
    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadLevel();
        }    
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
