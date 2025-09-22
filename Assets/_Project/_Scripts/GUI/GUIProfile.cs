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

    // Các phương thức khác để trống nếu không cần
    public void OnDieEvent(DieEvent eventType) { }
    public void OnLoseAHeartEvent(LoseAHeartEvent eventType) { }
    public void OnEarnCoinEvent(EarnCoinEvent eventType) { }
    public void OnGetAHeartEvent(GetAHeart eventType) { }
    public void OnLevelCompleteEvent(LevelCompleteEvent eventType) { }
}