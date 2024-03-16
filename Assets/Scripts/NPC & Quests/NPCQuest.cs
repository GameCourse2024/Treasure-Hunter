using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuest : MonoBehaviour
{
    [SerializeField]
    public QuestData questData;

    // Check if the quest is completed based on specific conditions
    public bool CheckQuestCompletion()
    {
        // Implement your specific conditions here
        return questData.isCompleted;
    }

    // Mark the quest as completed
    public void MarkQuestCompleted()
    {
        questData.isCompleted = true;
    }
}