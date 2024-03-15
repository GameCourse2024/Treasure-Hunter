using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManagerGamePlay : MonoBehaviour
{
    [SerializeField]
    private SoundForGame[] sounds;

    public static AudioManagerGamePlay Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (SoundForGame s in sounds)
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
        Debug.Log("Searching for sound: " + name);
        SoundForGame s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            Debug.Log("Playing Sound! " + name);
            s.source.Play();
        }
        else
        {
            Debug.Log("Error, no sound with that name");
        }
    }

    public void PlayQuestSound()
    {
        // Playing quest sound
        SoundForGame s = Array.Find(sounds, sound => sound.name == "QuestSound");
        s.source.Play();
    }
}
