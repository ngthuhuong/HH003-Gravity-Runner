using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailPanelController : GUIBase, IGUIComponent
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        GUIManager.Instance.RegisterGUIComponent("failpanel", this);
    }
    private void OnEnable()
    {
        if (closeButton == null)
        {
            Debug.LogError("Close button not found!");
        }
        else
        {
            // Gán sự kiện click
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(DeactivePanel);
        }
    }

    private void OnDisable()
    {
        // Hủy đăng ký khi scene unload
        GUIManager.Instance.UnregisterAllGUIComponents();
    }

    private void Start()
    {
        Hide();
    }

    public override void Show()
    {
        base.Show();
        Time.timeScale = 0;
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1;
    }

    public void DeactivePanel()
    {
        Hide();
        MMEventManager.TriggerEvent(new ResumeGameEvent());
    }

    public void SetEndLifeScreen()
    {
        if (text != null)
        {
            text.text = "You lose!";
        }
        Show();
        Debug.Log("Player has no hearts left!");
        // Có thể thêm sự kiện LoseGameEvent ở đây nếu cần
        // MMEventManager.TriggerEvent(new LoseGameEvent());
    }

    public void LevelComplete()
    {
        if (text != null)
        {
            text.text = "You complete the level!";
        }
        Show();
    }

    // Triển khai các phương thức từ IGUIComponent
    public void OnDieEvent(DieEvent eventType)
    {
        SetEndLifeScreen();
    }

    public void OnLoseAHeartEvent(LoseAHeartEvent eventType)
    {
        Show();
    }

    public void OnEarnCoinEvent(EarnCoinEvent eventType)
    {
        // FailPanel không cần xử lý EarnCoinEvent
    }

    public void OnGetAHeartEvent(GetAHeart eventType)
    {
        // FailPanel không cần xử lý GetAHeartEvent
    }

    public void OnEarnRewardEvent(EarnRewardEvent eventType)
    {
        // FailPanel không cần xử lý EarnRewardEvent
    }

    public void OnLevelCompleteEvent(LevelCompleteEvent eventType)
    {
        LevelComplete();
    }

    public void OnLoadedDataEvent(LoadedData eventType)
    {
        // Không cần cập nhật gì cho FailPanel khi dữ liệu load
    }
}