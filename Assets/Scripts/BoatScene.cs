using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScene : MonoBehaviour
{
    [SerializeField] private AudioManagerGamePlay audioManager;
    [SerializeField] private string themeSound;



    // Start is called before the first frame update
    void Start()
    {
        //audioManager.Play("BoatSound");
        AudioManagerGamePlay.Instance.Play(themeSound);        // Reduce player's health and update the health bar

        //AudioManagerGamePlay.Instance.Play(themeSound);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
