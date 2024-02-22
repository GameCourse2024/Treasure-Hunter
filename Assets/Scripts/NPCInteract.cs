using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NPCInteract : MonoBehaviour
{   
    [SerializeField]
    private TypeWriterEffect typeWriterEffect;
    [Tooltip("Refference to the text field that this npc effects")]
    [SerializeField]
    private TextMeshProUGUI textMeshProText;

    [Tooltip("The text that this NPC says")]
    [SerializeField]
    private string text;

    [Tooltip("Name of sound to play for quests")]
    [SerializeField]
    private string sound;
    [Tooltip("Name of sound effect for this character to play in interaction. NOTE list of sounds for npc can be found in the npc folder inside sounds")]
    [SerializeField]
    private string soundName;
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
    private bool questCompleted = false;
    [Tooltip("Time for banner display")]
    [SerializeField]
    private float bannerWaitTime;

    void Start() 
    {
        nPCBehaviour = GetComponent<NPCBehaviour>();
        typeWriterEffect.setText(text);    
    }
    private void Update() 
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Start Coroutine");
            StartCoroutine(InteractCoroutine());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set can interact flag to true
            canInteract = true;
            Debug.Log("Collided with: " + other.name);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set can't interact
            canInteract = false;
            Debug.Log("Un-Collided with: " + other.name);

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
        Debug.Log("Playing sound for NPC: " + soundName);
        AudioManagerGamePlay.Instance.Play(soundName);
    }
    private void ShowText()
    {
        Debug.Log("Turning on Canvas");
        canvas.gameObject.SetActive(true);
    }
    private void HideText()
    {
        Debug.Log("Turning off Canvas");
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
            Debug.Log("Calling Display Banner Function");
            ScrollController.DisplayBanner("Starting Quest: " + npcQuest.questData.name + "\nHint: " + hint, bannerWaitTime);
            
            return;
        }
        if(npcQuest.questData.isCompleted == true && !questCompleted)
        {
            questCompleted = true;
            Debug.Log("Giving Reward and changing NPC Text: " + npcQuest.questData.name);

            // TO DO REWARD PLAYER


            textMeshProText.SetText(afterQuestText);
             // Rolling out the banner
            Debug.Log("Calling Display Banner Function");
            ScrollController.DisplayBanner("Finished Quest: " + npcQuest.questData.name + "\nReward: " + hint, bannerWaitTime);
        }
     
    }
}
