using System;
using MoreMountains.Tools;
using UnityEngine;

public interface IGUIComponent
{
    void OnDieEvent(DieEvent eventType);
    void OnLoseAHeartEvent(LoseAHeartEvent eventType);
    void OnEarnCoinEvent(EarnCoinEvent eventType);
    void OnGetAHeartEvent(GetAHeart eventType);
    void OnEarnRewardEvent(EarnRewardEvent eventType);
    void OnLevelCompleteEvent(LevelCompleteEvent eventType);
    void OnLoadedDataEvent(LoadedData eventType);
}

public class GUIManager : Singleton<GUIManager>, MMEventListener<DieEvent>, MMEventListener<LoseAHeartEvent>, 
    MMEventListener<EarnCoinEvent>, MMEventListener<GetAHeart>, MMEventListener<EarnRewardEvent>, 
    MMEventListener<LevelCompleteEvent>, MMEventListener<LoadedData>
{
    private IGUIComponent guiHUD;
    private IGUIComponent guiFailPanel;
    private IGUIComponent guiPopup;
    private IGUIComponent guiProfile;

    private void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        this.MMEventStartListening<DieEvent>();
        this.MMEventStartListening<LoseAHeartEvent>();
        this.MMEventStartListening<EarnCoinEvent>();
        this.MMEventStartListening<GetAHeart>();
        this.MMEventStartListening<EarnRewardEvent>();
        this.MMEventStartListening<LevelCompleteEvent>();
        this.MMEventStartListening<LoadedData>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<DieEvent>();
        this.MMEventStopListening<LoseAHeartEvent>();
        this.MMEventStopListening<EarnCoinEvent>();
        this.MMEventStopListening<GetAHeart>();
        this.MMEventStopListening<EarnRewardEvent>();
        this.MMEventStopListening<LevelCompleteEvent>();
        this.MMEventStopListening<LoadedData>();
    }

    // Đăng ký một thành phần GUI
    public void RegisterGUIComponent(string componentName, IGUIComponent component)
    {
        switch (componentName.ToLower())
        {
            case "hud":
                guiHUD = component;
                Debug.Log("Registered GUIHUD");
                break;
            case "failpanel":
                guiFailPanel = component;
                Debug.Log("Registered GUIFailPanel");
                break;
            case "popup":
                guiPopup = component;
                Debug.Log("Registered GUIPopup");
                break;
            case "profile":
                guiProfile = component;
                Debug.Log("Registered GUIProfile");
                break;
            default:
                Debug.LogWarning($"Unknown GUI component: {componentName}");
                break;
        }

        // Nếu dữ liệu đã load, cập nhật ngay
        if (GameManager.Instance.isLoaded)
        {
            component.OnLoadedDataEvent(new LoadedData());
        }
    }

    // Hủy đăng ký tất cả thành phần GUI (khi chuyển scene)
    public void UnregisterAllGUIComponents()
    {
        guiHUD = null;
        guiFailPanel = null;
        guiPopup = null;
        guiProfile = null;
        Debug.Log("Unregistered all GUI components");
    }

    public void OnMMEvent(DieEvent eventType)
    {
        guiFailPanel?.OnDieEvent(eventType);
    }

    public void OnMMEvent(LoseAHeartEvent eventType)
    {
        guiHUD?.OnLoseAHeartEvent(eventType);
        guiFailPanel?.OnLoseAHeartEvent(eventType);
    }

    public void OnMMEvent(EarnCoinEvent eventType)
    {
        guiHUD?.OnEarnCoinEvent(eventType);
    }

    public void OnMMEvent(GetAHeart eventType)
    {
        guiHUD?.OnGetAHeartEvent(eventType);
    }

    public void OnMMEvent(EarnRewardEvent eventType)
    {
        guiPopup?.OnEarnRewardEvent(eventType);
    }

    public void OnMMEvent(LevelCompleteEvent eventType)
    {
        guiPopup?.OnLevelCompleteEvent(eventType);
    }

    public void OnMMEvent(LoadedData eventType)
    {
        guiHUD?.OnLoadedDataEvent(eventType);
        guiFailPanel?.OnLoadedDataEvent(eventType);
        guiPopup?.OnLoadedDataEvent(eventType);
        guiProfile?.OnLoadedDataEvent(eventType);
    }

    public void HidePopup()
    {
        guiPopup?.OnEarnRewardEvent(new EarnRewardEvent { /* Dữ liệu rỗng để ẩn */ });
        guiFailPanel?.OnLoseAHeartEvent(new LoseAHeartEvent { /* Dữ liệu rỗng để ẩn */ });
    }
}