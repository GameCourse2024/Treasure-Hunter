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
    }
}
