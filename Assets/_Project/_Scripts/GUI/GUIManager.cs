using System;
using MoreMountains.Tools;
using UnityEngine;

public class GUIManager : MonoBehaviour,MMEventListener<LoseAHeartEvent>
{
    [Header("UI Elements")]
    private GameObject guiHUD;
    private GameObject guiFailPanel;
    private MMEventListener<HitEvent> _mmEventListenerImplementation;

    void Start()
    {
        // Find children by name
        guiHUD = transform.Find("HUD")?.gameObject;
        guiFailPanel = transform.Find("FailPanel")?.gameObject;
        guiFailPanel.SetActive(false);
        // Check if the objects were found
        if (guiHUD == null)
        {
            Debug.LogError("GUIHUD not found!");
        }

        if (guiFailPanel == null)
        {
            Debug.LogError("GUIFailPanel not found!");
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening<LoseAHeartEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<LoseAHeartEvent>();
    }
    

    public void OnMMEvent(LoseAHeartEvent eventType)
    {
        guiFailPanel.SetActive(true);
    }
}