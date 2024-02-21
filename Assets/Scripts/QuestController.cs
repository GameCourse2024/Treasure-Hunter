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

}
