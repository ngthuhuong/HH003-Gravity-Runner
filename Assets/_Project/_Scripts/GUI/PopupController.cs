using System;
using System.Collections;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Định nghĩa PauseGameEvent

public class PopupController : GUIBase
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button backToHome;

    private void Awake()
    {
        GUIManager.Instance.RegisterGUIComponent("popup", this);
        Hide();
    }

    private void OnEnable()
    {

        if (closeButton == null || rewardText == null || backToHome == null)
        {
            Debug.LogError("PopupController: Missing required references.");
            return;
        }

        // Gán sự kiện click
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => Hide());
    }
    
    private void Start()
    {
        if (closeButton == null || rewardText == null || backToHome == null)
        {
            Debug.LogError("PopupController: Missing required references.");
            return;
        }
        backToHome.gameObject.SetActive(false);
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
    public void closeBoard()
    {
        Hide();
        Time.timeScale = 1;
        backToHome.gameObject.SetActive(false); 
        MMEventManager.TriggerEvent(new ResumeGameEvent());
    }

    public void ShowReward(EarnRewardEvent eventType)
    {
        if (rewardText != null)
        {
            if (eventType.reward.type == RewardType.Coin)
            {
                rewardText.text = $"You earned {eventType.reward.amount} coins!";
            }
            else if (eventType.reward.type == RewardType.Heart)
            {
                rewardText.text = "You earned a heart!";
            }
        }
        Show();
    }

    public void LevelComplete()
    {
        if (rewardText != null && GameManager.Instance.isLoaded)
        {
            rewardText.text = $"You complete level {GameManager.Instance.Level - 1}!";
        }
        backToHome.gameObject.SetActive(true);
        Show();
    }

    public void PauseGame()
    {
        if (rewardText != null)
        {
            rewardText.text = "Pause Game!";
        }
        backToHome.gameObject.SetActive(true);
        Show();
    }

    public void OnLevelCompleteEvent(LevelCompleteEvent eventType)
    {
        LevelComplete();
    }

   
}