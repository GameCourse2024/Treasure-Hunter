using UnityEngine;
using System.Collections;
using TMPro;

public class SkipCutScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skipText;
    [SerializeField] private float glowDuration = 5f;
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTriggerName = "SkipAnimation";

    private bool canSkip = false;
    private bool isGlowing = false;

    private void Start()
    {
        // Make the text initially invisible
        skipText.color = new Color(skipText.color.r, skipText.color.g, skipText.color.b, 0f);
    }

    private void Update()
    {
        if(!canSkip && !isGlowing)
        {
            StartCoroutine(GlowText());

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canSkip = true;
            Debug.Log("Triggering: " + animationTriggerName);
            animator.SetTrigger(animationTriggerName);
        }
    }

    private IEnumerator GlowText()
    {
        isGlowing = true;
        float timeElapsed = 0f;
        while (timeElapsed < glowDuration)
        {
            float t = timeElapsed / glowDuration;
            skipText.color = Color.Lerp(startColor, endColor, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is back to its original color and invisible
        skipText.color = new Color(skipText.color.r, skipText.color.g, skipText.color.b, 0f);
        isGlowing = false;
    }

    // This method is called by the animation event at the end of the skip animation
    public void OnSkipAnimationEnd()
    {
        SkipLevel();
    }

    private void SkipLevel()
    {
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
