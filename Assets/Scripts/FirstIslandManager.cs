using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstIslandManager : MonoBehaviour
{
    [Tooltip("Welcome message, delay before it shows")]
    [SerializeField]
    private float welcomeMessageDelayTime;
    [Tooltip("Welcome message")]
    [SerializeField]
    private string welcomeMessage;
    [SerializeField]
    private AudioManagerGamePlay audioManager;

    void Start()
    {
        // First we play the soundtrack
        Debug.Log("Playing themesong");
        audioManager.Play("ThemeSong");
        ScrollController.DisplayBanner(welcomeMessage, welcomeMessageDelayTime);
    }


}
