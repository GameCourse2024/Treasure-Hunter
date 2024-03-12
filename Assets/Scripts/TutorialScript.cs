using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button tutorialButton;

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private TextMeshProUGUI tmproText;

    [SerializeField]
    private TextMeshProUGUI explanationText;
    private int timesClicked = 0;

    [SerializeField]
    private string[] explanations = new string[5];

    public void PlayClicked()
    {
        Debug.Log("Play Button Clicked");
        tutorialButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        tmproText.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TutorialClicked()
    {
        Debug.Log("Tutorial Button Clicked");
        nextButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
    }

    public void NextClicked()
    {
        explanationText.gameObject.SetActive(true);
        timesClicked++;
    }
}
