using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NPCInteract : MonoBehaviour
{   
    [Tooltip("Delay between each letter typed for the typewriter effect")]
    [SerializeField]
    private float typewriterDelay = 0.05f;
    [SerializeField]
    private TypeWriterEffect typeWriterEffect;
    [Tooltip("Refference to the text field that this npc effects")]
    [SerializeField]
    private TextMeshProUGUI textMeshProText;

    [Tooltip("The text that this NPC says")]
    [SerializeField]
    private string text;
    [Tooltip("Name of sound effect for this character to play in interaction. NOTE list of sounds for npc can be found in the npc folder inside sounds")]
    [SerializeField] private string soundName;
    [Tooltip("How long for the co-routine to wait")]
    [SerializeField]
    private float waitTime = 10;
    private NPCBehaviour nPCBehaviour;
    private bool canInteract;
    [SerializeField]
    Canvas canvas;
    [Tooltip("Can this NPC give you a quest?")]
    [SerializeField]
    private bool canGiveQuest;
    [Tooltip("What quest does this NPC give?")]
    [SerializeField]
    private NPCQuest npcQuest;
    [Tooltip("What text does this NPC say after completing the quest")]
    [SerializeField]
    private string afterQuestText;
    [Tooltip("Hint for the player to complete the Quest")]
    [SerializeField]
    private string hint;
    [SerializeField]
    private bool questCompleted = false;
    [Tooltip("Time for banner display")]
    [SerializeField]
    private float bannerWaitTime;
    [Tooltip("How much gold does this NPC give after quest completion")]
    [SerializeField]
    private int gold;
    [Tooltip("Does this NPC Sell anything")]
    [SerializeField]
    private bool sells;
    [Tooltip("Has player spoken to this person, in other words ready to buy")]
    [SerializeField]
    private bool spoken;
    [Tooltip("NPC says when selling")]
    [SerializeField]
    private string textAfterSell;
    [Tooltip("Sell Price")]
    [SerializeField] private int sellPrice;
    [Tooltip("Did this NPC sell the ship")]
    [SerializeField] private bool ship;
    [Tooltip("If the player tries to buy without enough gold")]
    [SerializeField] private string textNotEnoughGold;

    [Tooltip("If the NPC sells something that leads to the next level , warn the player")]
    [SerializeField]
    private string warning;

    [Tooltip("Ship Cost")]
    [SerializeField] private int shipCost = 100;

    [SerializeField]
    private TextMeshProUGUI missionBarText;
    
    void Start() 
    {
       // Debug.Log("This is the text: " + text);
        nPCBehaviour = GetComponent<NPCBehaviour>();
    }
    private void Update() 
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Start Coroutine");
            StartCoroutine(InteractCoroutine());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set can interact flag to true
            canInteract = true;
           // Debug.Log("Collided with: " + other.name);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set can't interact
            canInteract = false;
           // Debug.Log("Un-Collided with: " + other.name);

        }
    }

    IEnumerator InteractCoroutine()
    {
        //Stop movement
        nPCBehaviour.StopMovement();
        //Play Sound
        PlaySound();
        // Give Quest
        QuestHandle();
        // Show the Text bubble on screen with dynamic text
        ShowText();
        yield return new WaitForSeconds(waitTime);

        // Resume movement
        nPCBehaviour.ResumeMovement();
        // Hide Text 
        HideText();

    }

    private void PlaySound()
    {
        //Debug.Log("Playing sound for NPC: " + soundName);
        AudioManagerGamePlay.Instance.Play(soundName);
    }
    private void ShowText()
    {
        Debug.Log("Showing Text");
        if(!sells)
        {
            canvas.gameObject.SetActive(true);
            typeWriterEffect.setText(text);    
            if(canGiveQuest && npcQuest.questData.isCompleted == true && questCompleted)
            {
                typeWriterEffect.setText(afterQuestText);
            }
            return;
        }
        if(sells && !spoken)
        {
            spoken = true;
            canvas.gameObject.SetActive(true);
            typeWriterEffect.setText(text);    

            if(ship)
            {
                ScrollController.DisplayBanner(warning,0);
            }
            return;
        }
        else
        {
            textMeshProText.SetText(textAfterSell);
            
            canvas.gameObject.SetActive(true);
            if(ship)
            {
                if(GoldManager.Instance.GetGold() >= shipCost)
                {
                    GoldManager.Instance.ShipBought(sellPrice);
                }
                else
                {
                    textMeshProText.SetText(textNotEnoughGold);

                    Debug.Log("Not enough gold!");
                }
            }
        }
    }
    private void HideText()
    {
        //Debug.Log("Turning off Canvas");
        canvas.gameObject.SetActive(false);
    }
    private void QuestHandle()
    {
        if(!canGiveQuest)
        {
            return;
        }
        QuestManager questManager = QuestManager.Instance;
        if (questManager == null)
        {
            return;
        }
        if(!npcQuest.questData.hasStarted)
        {
            Debug.Log("Giving Quest To Player: " + npcQuest.questData.name);
            questManager.AddQuest(npcQuest);
            // Rolling out the banner
           // Debug.Log("Calling Display Banner Function");
            ScrollController.DisplayBanner("Starting Quest: " + npcQuest.questData.name + "\nHint: " + hint, bannerWaitTime);
            // Updating the quests scroll
            string originalText = missionBarText.text;
            string modifiedText = "<color=yellow>" + originalText + "</color>";
            missionBarText.text = modifiedText;
            return;
        }
        if(npcQuest.questData.isCompleted == true && !questCompleted)
        {
            questCompleted = true;
            //Debug.Log("Giving Reward and changing NPC Text: " + npcQuest.questData.name);

            // REWARD PLAYER
            GoldManager.Instance.AddGold(gold);

             // Rolling out the banner
            //Debug.Log("Calling Display Banner Function");
            ScrollController.DisplayBanner("Finished Quest: " + npcQuest.questData.name + "\nReward: " + gold.ToString() + " Gold", bannerWaitTime);

            // Crossing out on the sidebar
            if (missionBarText == null)
            {
                return;
            }
            else
            {
                string originalText = missionBarText.text;
                string modifiedText = "<color=yellow><s>" + originalText + "</s></color>";
                missionBarText.text = modifiedText;
            }

        }
     
    }
}
