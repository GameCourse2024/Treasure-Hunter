using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [Tooltip("Delay between each letter typed")]
    [SerializeField]
    public float delay = 0.1f;
    
    [Tooltip("The text that this NPC says")]
    [SerializeField]
    private string fullText = "Default text";

    private string currentText = "";
    private TMP_Text textComponent;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        textComponent.text = "";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}