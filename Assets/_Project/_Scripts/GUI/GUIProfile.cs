using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIProfile : GUIBase
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void OnEnable()
    {
        GUIManager.Instance.RegisterGUIComponent("profile", this);
    }

    private void OnDisable()
    {
    }

    private void Start()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {GameManager.Instance.CoinCount}";
        }
        if (levelText != null)
        {
            levelText.text = $"Level: {GameManager.Instance.Level}";
        }
    }

    public void OnLoadedDataEvent(LoadedData eventType)
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {GameManager.Instance.CoinCount}";
        }
        if (levelText != null)
        {
            levelText.text = $"Level: {GameManager.Instance.Level}";
        }
    }

}