using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x=> x.name==name);

        if(s == null)
        {
            Debug.Log("Error no music like that found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
         Sound s = Array.Find(sfxSounds, x=> x.name==name);

        if(s == null)
        {
            Debug.Log("Error no SFX like that found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

}
