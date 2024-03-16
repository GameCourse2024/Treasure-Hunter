using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillQuest : MonoBehaviour
{
    private static KillQuest _instance;
    public static KillQuest Instance { get { return _instance; } }

    private int npcKilledCount;
    private int npcKilledSinceQuestStart;
    [Tooltip("Time for banner to display")]
    [SerializeField]
    private float bannerWaitTime;
    [SerializeField]
    private string questName = "3D Cube destination";
    [SerializeField]
    private int killsForQuest;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void IncrementNpcKilledCount()
    {
        npcKilledCount++;

        if (!QuestManager.Instance.CheckQuestStarted(questName))
        {
            return;
        }
        else
        {
            npcKilledSinceQuestStart++;
            Debug.Log("NPC Killed: " + npcKilledSinceQuestStart);
            if (npcKilledSinceQuestStart == killsForQuest)
            {
                Debug.Log("Killed 3 NPC");
                QuestManager.Instance.CompleteQuest(questName);
                Debug.Log("Calling Display Banner Function");
                ScrollController.DisplayBanner("Quest Updated: " + questName + "\nHint: Return to who sent you here", bannerWaitTime);
            }
        }


    }

    public int GetNpcKilledCount()
    {
        return npcKilledCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        npcKilledCount = 0;
        npcKilledSinceQuestStart = 0;
    }
}
