using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIProfile : GUIBase
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        if(GameManager.Instance.isLoaded){
            UpdateCoinText(GameManager.Instance.CoinCount);
            UpdateLevelText(GameManager.Instance.Level);
        }
    }

    public void UpdateCoinText(int coinCount)
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {coinCount}";
        }
    }

    public void UpdateLevelText(int level)
    {
        if (levelText != null)
        {
            levelText.text = $"Level: {level}";
        }
    }

    private void Awake()
    {
        Show();
    }
}