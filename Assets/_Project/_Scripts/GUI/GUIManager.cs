using System;
using MoreMountains.Tools;
using UnityEditor.Search;
using UnityEngine;
public class GUIManager : Singleton<GUIManager>, 
    MMEventListener<LoadedData>, 
    MMEventListener<LoseAHeartEvent>,MMEventListener<DieEvent>,MMEventListener<PauseGameEvent>,MMEventListener<LevelCompleteEvent>,MMEventListener<EarnRewardEvent>
{
    private GUIHUD_Controller guiHUD;
    private FailPanelController guiFailPanel;
    private PopupController guiPopup;
    private GUIProfile guiProfile;
    private void OnEnable()
    {
        this.MMEventStartListening<LoadedData>();
        this.MMEventStartListening<LoseAHeartEvent>();
        this.MMEventStartListening<DieEvent>();
        this.MMEventStartListening<PauseGameEvent>();
        this.MMEventStartListening<LevelCompleteEvent>();
        this.MMEventStartListening<EarnRewardEvent>();
    }
    
    private void OnDisable()
    {
        this.MMEventStopListening<LoadedData>();
        this.MMEventStopListening<LoseAHeartEvent>();
        this.MMEventStopListening<DieEvent>();
        this.MMEventStopListening<PauseGameEvent>();
        this.MMEventStopListening<LevelCompleteEvent>();
        this.MMEventStopListening<EarnRewardEvent>();
    }

    public void RegisterGUIComponent(string componentName, GUIBase component)
    {
        switch (componentName.ToLower())
        {
            case "hud":
                guiHUD = component as GUIHUD_Controller;
                break;
            case "failpanel":
                guiFailPanel = component as FailPanelController;
                break;
            case "popup":
                guiPopup = component as PopupController;
                break;
            case "profile":
                guiProfile = component as GUIProfile;
                break;
        }
    }

    public void UnregisterGUIComponent(string componentName, GUIBase component)
    {
        switch (componentName.ToLower())
        {
            case "hud":
                if (guiHUD == component) guiHUD = null;
                break;
            case "failpanel":
                if (guiFailPanel == component) guiFailPanel = null;
                break;
            case "popup":
                if (guiPopup == component) guiPopup = null;
                break;
            case "profile":
                if (guiProfile == component) guiProfile = null;
                break;
        }
    }

    public void OnMMEvent(LoadedData eventType)
    {
    }

    public void OnMMEvent(LoseAHeartEvent eventType)
    {
        
        guiFailPanel.OnHandleLoseAHeart();
    }

    public void OnMMEvent(DieEvent eventType)
    {
        guiFailPanel.SetEndLifeScreen();
    }
    
    
    public void OnMMEvent(LevelCompleteEvent eventType)
    {
        guiPopup.LevelComplete();
    }

    public void OnMMEvent(EarnRewardEvent eventType)
    {
        guiPopup.ShowReward(eventType);
    }

    public void OnMMEvent(PauseGameEvent eventType)
    {
        guiPopup.PauseGame();
    }
}
