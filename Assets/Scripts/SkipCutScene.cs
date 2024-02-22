using UnityEngine;
using System.Collections;
using TMPro;

public class SkipCutScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skipText;
    [SerializeField] private float glowDuration = 5f;
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color endColor = Color.red;

    private bool canSkip = false;

    private void Start()
    {
        // Make the text initially invisible
        skipText.color = new Color(skipText.color.r, skipText.color.g, skipText.color.b, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canSkip)
            {
                // Skip to the next level
                int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
                if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
                }
            }
            else
            {
                StartCoroutine(GlowText());
            }
        }
    }

    private IEnumerator GlowText()
    {
        canSkip = true; // Allow skipping 
        float timeElapsed = 0f;
        bool increasing = true;
        while (timeElapsed < glowDuration)
        {
            float t = timeElapsed / glowDuration;
            if (increasing)
            {
                skipText.color = Color.Lerp(startColor, endColor, t);
            }
            else
            {
                skipText.color = Color.Lerp(endColor, startColor, t);
            }

            if (t >= 1f)
            {
                increasing = !increasing;
                timeElapsed = 0f;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is back to its original color and invisible
        skipText.color = new Color(skipText.color.r, skipText.color.g, skipText.color.b, 0f);
        canSkip = false;
    }
}
