using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    [SerializeField] 
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI questText;

    [Tooltip("Starting Amount")]
    [SerializeField]
    private int gold = 0;
    [SerializeField]
    private string enoughGoldForShip;
    [SerializeField]
    private float waitTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        UpdateGoldText();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldText();
        if (gold >= 300)
        {
            ScrollController.DisplayBanner(enoughGoldForShip, waitTime);
            string originalText = questText.text;
            string modifiedText = "<color=yellow><s>" + originalText + "</s></color>";
            questText.text = modifiedText;
        }
    }

    public void RemoveGold(int amount)
    {
        gold = Mathf.Max(0, gold - amount);
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + gold.ToString();
        }
    }

    public void ShipBought(int amount)
    {
        Debug.Log("In the ship bought function");
        gold -= amount;
        UpdateGoldText();

        // Fade To Next Level
        LevelTransition.Instance.FadeToNextLevel();
    }

    public int GetGold()
    {
        return gold;
    }
}
