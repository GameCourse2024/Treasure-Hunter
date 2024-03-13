using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitToSkip : MonoBehaviour
{
    [SerializeField] 
    private float waitTime = 5f;
    void Start()
    {
        StartCoroutine(WaitAndSwitch());
    }

     private IEnumerator WaitAndSwitch()
    {
        yield return new WaitForSeconds(waitTime);
        SwitchToNextScene();
    }

    private void SwitchToNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

