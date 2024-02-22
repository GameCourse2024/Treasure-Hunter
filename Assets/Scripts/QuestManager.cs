using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance { get { return instance; } }

    [SerializeField]
    private List<NPCQuest> npcQuests = new List<NPCQuest>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Duplicate instance of QuestManager found. Destroying the duplicate.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
        ResetAllQuests();
    }

    // Add a quest to the list
    public void AddQuest(NPCQuest npcQuest)
    {
        Debug.Log("Started quest: " + npcQuest.questData.name);
        npcQuests.Add(npcQuest);
        npcQuest.questData.hasStarted = true;
    }

    // Remove a quest from the list
    public void RemoveQuest(NPCQuest npcQuest)
    {
        npcQuests.Remove(npcQuest);
    }

   // Mark a quest as completed based on its name
    public void CompleteQuest(string questName)
    {
        Debug.Log("Searching for completed quest: " + questName);
        foreach (NPCQuest npcQuest in npcQuests)
        {
            Debug.Log("QuestName found: " + npcQuest.questData.questName);
  
            if (npcQuest.questData.questName == questName)
            {
                Debug.Log("Quest found");
                if(npcQuest.CheckQuestCompletion())
                {
                    Debug.Log("Quest has been completed.");
                    return;
                }
                npcQuest.MarkQuestCompleted();
                Debug.Log("Quest completed: " + questName);
                return;
            }
        }
    }
    private void ResetAllQuests()
    {
        foreach (NPCQuest npcQuest in npcQuests)
        {
            npcQuest.questData.ResetState();
        }
    }
    public bool CheckQuestStarted(string questName)
    {
        foreach (NPCQuest npcQuest in npcQuests)
        {
            if (npcQuest.questData.questName == questName)
            {
                return npcQuest.questData.hasStarted;
            }
        }
        Debug.Log("No quest like that found");
        return false;
    }
}
