using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    [SerializeField]
    private AudioManagerGamePlay audioManager;
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

    void Start() 
    {
        nPCBehaviour = GetComponent<NPCBehaviour>();
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
        nPCBehaviour.StopMovement();
        Debug.Log("Playing sound for NPC: " + soundName);
        audioManager.Play(soundName);

        // Show the Text bubble on screen with dynamic text
        Debug.Log("Turning on Canvas");
        canvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        Debug.Log("Resuming NPC Movement");
        nPCBehaviour.ResumeMovement();
        Debug.Log("Turning off Canvas");
        canvas.gameObject.SetActive(false);

        // Stop Talking Sounds, remove text bubble

    }
}
