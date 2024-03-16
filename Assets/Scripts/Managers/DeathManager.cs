using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private static DeathManager instance;
    public static DeathManager Instance { get { return instance; } }

    [SerializeField] 
    private TextMeshProUGUI deathText;
    [SerializeField]
    private float fadeTime;
    [Tooltip("The main death text")]
    [SerializeField]
    private string death;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void FadeOut(string text)
    {
        Debug.Log("Showing Death Text");
        StartCoroutine(FadeInRoutine(text));
    }

    private IEnumerator FadeInRoutine(string text)
    {
        deathText.text = death + "\n" + text;

        float elapsedTime = 0f;
        Color startColor = deathText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeTime)
        {
            float t = elapsedTime / fadeTime;
            deathText.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Punish player

        deathText.color = endColor;

        // Reload this scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    }
}
