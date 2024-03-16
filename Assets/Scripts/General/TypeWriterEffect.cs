using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    private string fullText = "Default text";
    private TMP_Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        Debug.Log("I'm Here " + textComponent);
        textComponent.text = fullText;
    }

    public void setText(string newText)
    {
        fullText = newText;
        textComponent.text = fullText; // Update the text immediately when set
    }
}