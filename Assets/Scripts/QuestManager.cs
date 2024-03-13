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
        foreach (NPCQuest npcQuest in npcQuests)
        {

            if (npcQuest.questData.questName == questName)
            {
                if(npcQuest.CheckQuestCompletion())
                {
                    return;
                }
                npcQuest.MarkQuestCompleted();
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
        return false;
    }
}
