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
    private int timesClicked = 1;

    [SerializeField] 
    private Image explanationImage;


    [SerializeField]
    private string[] explanations = new string[5];
    [SerializeField]
    private Sprite[] images = new Sprite[5];

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
        explanationText.gameObject.SetActive(true);
        explanationImage.gameObject.SetActive(true);

    }

    public void NextClicked()
    {
        if(timesClicked == explanations.Length)
        {
            explanationImage.sprite = images[0];
            explanationText.text = explanations[0];
            explanationText.gameObject.SetActive(false);
            explanationImage.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            tutorialButton.gameObject.SetActive(true);
            timesClicked = 0;
            return;
        }
        Debug.Log("Next Clicked");
        explanationText.gameObject.SetActive(true);
        explanationImage.gameObject.SetActive(true);
        explanationText.text = explanations[timesClicked];
        explanationImage.sprite = images[timesClicked];
        timesClicked++;
    }
}
