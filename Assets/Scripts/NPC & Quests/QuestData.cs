using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Data", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questName;
    public string questDescription;
    public bool isCompleted;
    public bool hasStarted;
    public void ResetState()
    {
        isCompleted = false;
        hasStarted = false;
    }
}