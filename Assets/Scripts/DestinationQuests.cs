using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationQuests : MonoBehaviour
{
    [SerializeField]
    private string questName = "3D Cube destination";

    [SerializeField]
    private float triggerSize = 1.0f;

    private BoxCollider boxCollider;

    private void Start()
    {
        // Get the BoxCollider component attached to the GameObject
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider component not found on GameObject.");
        }
        else
        {
            boxCollider.size = new Vector3(triggerSize, triggerSize, triggerSize);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");
        if (other.CompareTag("Player"))
        {
            Debug.Log(name +" , has collided with player");
            QuestManager questManager = QuestManager.Instance; // Assuming Instance is your singleton instance property
            if (questManager != null)
            {
                Debug.Log("Calling quest manager for: " + questName);
                questManager.CompleteQuest(questName);
            }
        }
    }

}
