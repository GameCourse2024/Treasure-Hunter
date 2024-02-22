using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationQuests : MonoBehaviour
{
    [Tooltip("Time for banner to display")]
    [SerializeField]
    private float bannerWaitTime;
    [SerializeField]
    private string questName = "3D Cube destination";

    [SerializeField]
    private float triggerSize = 1.0f;
    [Tooltip("Name of sound you want to play")]
    [SerializeField]
    private string soundName;

    private BoxCollider boxCollider;
    private bool hasVisited = false;

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
        Debug.Log(name + " , has collided with player");
        QuestManager questManager = QuestManager.Instance; // Assuming Instance is your singleton instance property
        if (questManager != null)
        {
            if (!questManager.CheckQuestStarted(questName))
            {
                Debug.Log("Quest has not been started yet");
                return;
            }

            if (!hasVisited)
            {
                hasVisited = true;
                Debug.Log("Calling quest manager for: " + questName);
                questManager.CompleteQuest(questName);
                AudioManagerGamePlay.Instance.PlayQuestSound();
                // Rolling out the banner
                Debug.Log("Calling Display Banner Function");
                ScrollController.DisplayBanner("Quest Updated: " + questName + "\nHint: Return to who sent you here", bannerWaitTime);
            }

        }
    }
}

}
