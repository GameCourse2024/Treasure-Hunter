using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneSoundManager : MonoBehaviour
{
    [SerializeField]
    private string[] sfxName;
    [SerializeField]
    private string[] soundName;
    [SerializeField]
    private float[] delays;

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
    }
}
