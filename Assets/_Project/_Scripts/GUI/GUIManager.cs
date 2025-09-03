using System;
using MoreMountains.Tools;
using UnityEngine;

public class GUIManager : MonoBehaviour,MMEventListener<DieEvent>,MMEventListener<LoseAHeartEvent>, MMEventListener<EarnCoinEvent>,MMEventListener<GetAHeart>, MMEventListener<EarnRewardEvent>, MMEventListener<LevelCompleteEvent>
{
    [Header("UI Elements")]
    private GUIHUD_Controller guiHUD;
    private FailPanelController guiFailPanel;
    private PopupController guiPopup;
   

    void Start()
    {
        // Find children by name
        guiHUD = transform.Find("HUD")?.GetComponent<GUIHUD_Controller>();
        guiFailPanel = transform.Find("FailPanel")?.GetComponent<FailPanelController>();
        guiPopup = transform.Find("RewardPopup")?.GetComponent<PopupController>();
       
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
        this.MMEventStartListening<EarnRewardEvent>();
        this.MMEventStartListening<LevelCompleteEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EarnCoinEvent>();
        this.MMEventStopListening<LoseAHeartEvent>();
        this.MMEventStopListening<DieEvent>();
        this.MMEventStopListening<EarnRewardEvent>();
        this.MMEventStopListening<LevelCompleteEvent>();
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
        guiHUD.UpdateCoinText();
    }

    public void OnMMEvent(GetAHeart eventType)
    {
        guiHUD.ShowAHeart();
    }

    public void OnMMEvent(EarnRewardEvent eventType)
    {
        guiPopup.ShowReward(eventType);
    }

    public void OnMMEvent(LevelCompleteEvent eventType)
    {
        guiPopup.LevelComplete();
    }
}