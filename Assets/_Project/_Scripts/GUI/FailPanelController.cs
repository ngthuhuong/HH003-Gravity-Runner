using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailPanelController : GUIBase
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.RegisterGUIComponent("failpanel", this);
            Hide();
        }
        else
        {
            Debug.LogError("FailPanelController: GUIManager not found!");
        }
    }
    private void Start()
    {
        if (closeButton == null)
        {
            Debug.LogError("Close button not found!");
        }
        else
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(DeactivePanel);
        }
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
    public void SetText(string message)
    {
        if (text != null)
        {
            text.text = message;
        }
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
    
    public void OnLoadedDataEvent(LoadedData eventType)
    {
        // Không cần cập nhật gì cho FailPanel khi dữ liệu load
    }

    public void OnHandleLoseAHeart()
    {
        text.text = $"You lose a heart!";
        Show();
    }
}