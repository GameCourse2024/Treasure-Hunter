using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    [Tooltip("How long for the co-routine to wait")]
    [SerializeField]
    private float waitTime = 10;
    private NPCBehaviour nPCBehaviour;
    private bool canInteract;

    void Start() 
    {
        nPCBehaviour = GetComponent<NPCBehaviour>();
    }

    private void Update() 
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            nPCBehaviour.StopMovement();
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
        yield return new WaitForSeconds(waitTime);

        // Play As If Talking Sound
        // Show the Text bubble on screen with dynamic text
        nPCBehaviour.ResumeMovement();

        // Stop Talking Sounds, remove text bubble

    }
}
