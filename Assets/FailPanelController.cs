using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Correct namespace for UI Button

public class FailPanelController : MonoBehaviour
{
    private Button closeButton;

    void Start()
    {
        closeButton = gameObject.GetComponent<Button>(); // Access UnityEngine.UI.Button
    }
    
}