using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : GUIBase
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI rewardText;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(() => Hide());
        Hide();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void ShowReward(EarnRewardEvent eventType)
    {
        if(eventType.reward.type == RewardType.Coin)
        {
            rewardText.text = $"You earned {eventType.reward.amount} coins!";
        }
        else if(eventType.reward.type == RewardType.Heart)
        {
            rewardText.text = "You earned a heart!";
        }
        Show();
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        rewardText.text = "Pause Game!";
        Show();
        Time.timeScale = 0;
    }
  
}
