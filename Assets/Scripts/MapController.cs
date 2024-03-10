using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Canvas mapCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            mapCanvas.gameObject.SetActive(!mapCanvas.gameObject.activeSelf);
        }
    }
}