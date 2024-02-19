using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManagerGamePlay : MonoBehaviour
{
    [SerializeField]
    private SoundForGame[] sounds;

    void Awake()
    {
        foreach(SoundForGame s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = false;
            s.source.loop = s.loop;

        }
    }

    public void Play(string name)
    {
       Debug.Log("Searching for song:" + name);
       SoundForGame s = Array.Find(sounds, sound => sound.name == name);
       if(s!=null)
       {
            Debug.Log("Found Sound: " + s.name);
            s.source.Play();
            Debug.Log("Playing sound: " + s.name);
       }
       else
       {
        Debug.Log("Error, no sound with that name");
       }
    }
}
