using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstIslandManager : MonoBehaviour
{
    [SerializeField]
    private AudioManagerGamePlay audioManager;
    // Start is called before the first frame update
    void Start()
    {
        // First we play the soundtrack
        Debug.Log("Playing themesong");
        audioManager.Play("Music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
