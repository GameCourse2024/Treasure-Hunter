using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private static QuestController instance;
    public static QuestController Instance { get { return instance; } }

    [SerializeField]
    private List<QuestData> allQuests = new List<QuestData>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Duplicate instance of QuestController found. Destroying the duplicate.");
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

    public void ResetAllQuests()
    {
        foreach (QuestData quest in allQuests)
        {
            quest.ResetState();
        }
    }

    public List<QuestData> GetFinishedQuests()
    {
        return allQuests.FindAll(q => q.isCompleted);
    }

    public List<QuestData> GetStartedQuests()
    {
        return allQuests.FindAll(q => q.hasStarted && !q.isCompleted);
    }

    public List<QuestData> GetNotStartedQuests()
    {
        return allQuests.FindAll(q => !q.hasStarted);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            List<QuestData> startedQuests = GetStartedQuests();
            Debug.Log("Started Quests:");
            foreach (QuestData quest in startedQuests)
            {
                Debug.Log("- " + quest.questName);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            List<QuestData> finishedQuests = GetFinishedQuests();
            Debug.Log("Finished Quests:");
            foreach (QuestData quest in finishedQuests)
            {
                Debug.Log("- " + quest.questName);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            List<QuestData> notStartedQuests = GetNotStartedQuests();
            Debug.Log("Not Started Quests:");
            foreach (QuestData quest in notStartedQuests)
            {
                Debug.Log("- " + quest.questName);
            }
        }
    }
}
