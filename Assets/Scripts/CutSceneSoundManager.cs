using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneSoundManager : MonoBehaviour
{
    [SerializeField]
    private string[] sfxName;
    [SerializeField]
    private string[] soundName;
    [SerializeField]
    private float[] delays;
    [SerializeField]
    private float timeForScene = 23;

    void Start() 
    {
        AudioManager.Instance.PlayMusic(soundName[0]);
        StartCoroutine(PlaySfxSequence());
    }

    IEnumerator PlaySfxSequence()
    {
        for (int i = 0; i < sfxName.Length; i++)
        {
            yield return new WaitForSeconds(delays[i]);
            AudioManager.Instance.PlaySFX(sfxName[i]);
        }

        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        Debug.Log("Start next level co routine");
        yield return new WaitForSeconds(timeForScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
