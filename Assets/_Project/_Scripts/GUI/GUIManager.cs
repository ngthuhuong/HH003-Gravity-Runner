using System;
using MoreMountains.Tools;
using UnityEngine;

public class GUIManager : MonoBehaviour,MMEventListener<DieEvent>,MMEventListener<LoseAHeartEvent>, MMEventListener<EarnCoinEvent>
{
    [Header("UI Elements")]
    private GUIHUD_Controller guiHUD;
    private FailPanelController guiFailPanel;
   

    void Start()
    {
        // Find children by name
        guiHUD = transform.Find("HUD")?.GetComponent<GUIHUD_Controller>();
        guiFailPanel = transform.Find("FailPanel")?.GetComponent<FailPanelController>();
        guiFailPanel.Hide();
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
        this.MMEventStartListening<EarnCoinEvent>();
        this.MMEventStartListening<DieEvent>();
        this.MMEventStartListening<LoseAHeartEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EarnCoinEvent>();
        this.MMEventStopListening<LoseAHeartEvent>();
        this.MMEventStopListening<DieEvent>();
    }


    public void OnMMEvent(DieEvent eventType)
    {
        guiFailPanel.SetEndLifeScreen();
    }

    public void OnMMEvent(LoseAHeartEvent eventType)
    {
        guiHUD.HideAHeart();
        guiFailPanel.Show();
    }

    public void OnMMEvent(EarnCoinEvent eventType)
    {
        Debug.Log("Earned coins");
    }
}