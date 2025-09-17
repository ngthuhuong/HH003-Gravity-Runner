using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class GUIHUD_Controller : MonoBehaviour, IGUIComponent
{
    [SerializeField] private TextMeshProUGUI coinText; // Reference to the Text component for coin count
    [SerializeField] private TextMeshProUGUI levelText; // Optional: Text for level display
    [SerializeField] private GameObject heartPrefab; // Prefab for the heart icon
    [SerializeField] private Transform healthRoot; // Parent object to hold heart icons

    private int maxHearts = 3; // Maximum number of hearts
    private int currentHearts; // Current number of hearts
    public int CurrentHearts
    {
        get { return currentHearts; }
        private set { currentHearts = value; }
    }
    private readonly List<GameObject> heartObjects = new List<GameObject>(); // Cache for heart objects
    
    private void OnEnable()
    {
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.RegisterGUIComponent("hud", this);
        }
        else
        {
            Debug.LogError("GUIManager.Instance is null. Ensure GUIManager is initialized in the scene.");
        }
    }

    private void OnDisable()
    {
        // Hủy đăng ký khi scene unload
        GUIManager.Instance.UnregisterAllGUIComponents();
    }

    private void Start()
    {
        if (coinText == null || healthRoot == null || heartPrefab == null)
        {
            Debug.LogError("GUIHUD_Controller: Missing required references.");
            return;
        }
        InitializeHearts();
        SetHearts(maxHearts); // Đặt số lượng heart ban đầu
    }

    private void InitializeHearts()
    {
        // Tạo các heart object chỉ một lần
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, healthRoot);
            heartObjects.Add(heart);
        }
    }

    public void SetHearts(int hearts)
    {
        // Giới hạn số lượng heart trong khoảng hợp lệ
        currentHearts = Mathf.Clamp(hearts, 0, maxHearts);

        // Cập nhật trạng thái active của mỗi heart
        for (int i = 0; i < heartObjects.Count; i++)
        {
            heartObjects[i].SetActive(i < currentHearts);
        }
    }

    public void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = GameManager.Instance.CoinCount.ToString();
        }
    }

    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = $"Level: {GameManager.Instance.Level}";
        }
    }

    public void HideAHeart()
    {
        if (currentHearts > 0)
        {
            heartObjects[currentHearts - 1].SetActive(false); // Vô hiệu hóa heart cuối cùng
            currentHearts--;

            if (currentHearts == 0)
            {
                MMEventManager.TriggerEvent(new DieEvent());
            }
        }
    }

    public void ShowAHeart()
    {
        if (currentHearts < maxHearts)
        {
            heartObjects[currentHearts].SetActive(true); // Kích hoạt heart tiếp theo
            currentHearts++;
        }
    }

    // Triển khai các phương thức từ IGUIComponent
    public void OnDieEvent(DieEvent eventType)
    {
        // HUD không cần xử lý DieEvent (FailPanel sẽ xử lý)
    }

    public void OnLoseAHeartEvent(LoseAHeartEvent eventType)
    {
        HideAHeart();
    }

    public void OnEarnCoinEvent(EarnCoinEvent eventType)
    {
        UpdateCoinText();
    }

    public void OnGetAHeartEvent(GetAHeart eventType)
    {
        ShowAHeart();
    }

    public void OnEarnRewardEvent(EarnRewardEvent eventType)
    {
        // HUD không cần xử lý EarnRewardEvent (Popup sẽ xử lý)
    }

    public void OnLevelCompleteEvent(LevelCompleteEvent eventType)
    {
        // HUD không cần xử lý LevelCompleteEvent (Popup sẽ xử lý)
        UpdateLevelText(); // Cập nhật level nếu có
    }

    public void OnLoadedDataEvent(LoadedData eventType)
    {
        UpdateCoinText();
        UpdateLevelText();
    }
}