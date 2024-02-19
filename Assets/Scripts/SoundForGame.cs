using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundForGame
{
    [SerializeField]
    public string name;
    [SerializeField]
    public AudioClip clip;
    [SerializeField]
    [Range(0f,1f)]
    public float volume;
    [SerializeField]
    [Range(.1f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;

}
