using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreiveQuest : MonoBehaviour
{
    [Tooltip("Time for banner to display")]
    [SerializeField]
    private float bannerWaitTime;
    [SerializeField]
    private string questName = "3D Cube destination";

    [SerializeField]
    private float triggerSize = 1.0f;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!QuestManager.Instance.CheckQuestStarted(questName))
                {
                    return;
                }

                if (!hasVisited)
                {
                    hasVisited = true;
                    QuestManager questManager = QuestManager.Instance; // Assuming Instance is your singleton instance property
                    if (questManager != null)
                    {
                        questManager.CompleteQuest(questName);
                        // Rolling out the banner
                        ScrollController.DisplayBanner("Quest Updated: " + questName + "\nHint: Return to who sent you here", bannerWaitTime);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
