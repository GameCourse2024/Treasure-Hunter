using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    [SerializeField] 
    private TextMeshProUGUI goldText;

    [Tooltip("Starting Amount")]
    [SerializeField]
    private int gold = 0;

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
}
