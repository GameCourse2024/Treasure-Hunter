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
    [SerializeField] private AudioManagerGamePlay audioManager;
    [SerializeField] private Canvas missionCanvas;
    private bool canvasVisible = true;
    void Start()
    {
        // First we play the soundtrack
        Debug.Log("Playing themesong");
        audioManager.Play("ThemeSong");
        ScrollController.DisplayBanner(welcomeMessage, welcomeMessageDelayTime);
    }

    private void Update()
    {
        // Toggle canvas visibility when 'O' key is pressed
        if (Input.GetKeyDown(KeyCode.O))
        {
            canvasVisible = !canvasVisible;
            missionCanvas.enabled = canvasVisible;
            missionCanvas.GetComponent<CanvasGroup>().alpha = canvasVisible ? 1 : 0;
        }
    }


}
