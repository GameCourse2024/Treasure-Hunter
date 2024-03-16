using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScrollController : MonoBehaviour
{
    [SerializeField] 
    private Image scrollImage;
    [SerializeField] 
    private TextMeshProUGUI displayTextComponent;
    [Tooltip("Time for scroll to roll out")]
    [SerializeField] 
    private float lerpDuration = 1f;
    [Tooltip("Time before the scroll rolls back")]
    [SerializeField] 
    private float rollbackDelay = 5f;

    private float targetScale = 1f;
    private float currentScale = 0f;
    private Coroutine activeCoroutine; // Track the active coroutine

    private static ScrollController instance;

    public static ScrollController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public static void DisplayBanner(string text, float delay)
    {
        if (instance != null)
        {
            // Stop the active coroutine if it exists
            if (instance.activeCoroutine != null)
            {
                instance.StopCoroutine(instance.activeCoroutine);
            }
            // Start the new coroutine
            instance.activeCoroutine = instance.StartCoroutine(instance.DelayedScrollRoutine(text, delay));
        }
    }

    private IEnumerator DelayedScrollRoutine(string text, float delay)
    {
        yield return new WaitForSeconds(delay);

        yield return StartCoroutine(ScrollRoutine(text));

        yield return new WaitForSeconds(rollbackDelay);

        yield return StartCoroutine(RollbackScroll());

        activeCoroutine = null; // Reset the active coroutine
    }

    private IEnumerator ScrollRoutine(string text)
    {
        yield return StartCoroutine(LerpScroll());

        yield return StartCoroutine(DisplayTextOverTime(text));
    }

    private IEnumerator LerpScroll()
    {
        AudioManagerGamePlay.Instance.PlayQuestSound();
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            currentScale = Mathf.Lerp(0f, targetScale, timeElapsed / lerpDuration);
            scrollImage.transform.localScale = new Vector3(currentScale, 1f, 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        scrollImage.transform.localScale = new Vector3(targetScale, 1f, 1f);
    }

    private IEnumerator DisplayTextOverTime(string text)
    {
        displayTextComponent.text = text;
        displayTextComponent.color = new Color(1f, 1f, 1f, 1f);

        yield return null;
    }

    private IEnumerator RollbackScroll()
    {
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            currentScale = Mathf.Lerp(targetScale, 0f, timeElapsed / lerpDuration);
            scrollImage.transform.localScale = new Vector3(currentScale, 1f, 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        scrollImage.transform.localScale = new Vector3(0f, 1f, 1f);
        displayTextComponent.text = "";
    }
}
