using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class GUIHUD_Controller : MonoBehaviour, MMEventListener<EarnCoinEvent>, MMEventListener<HitEvent>
{
    public TextMeshProUGUI coinText; // Reference to the Text component for coin count
    public GameObject heartPrefab; // Prefab for the heart icon
    [SerializeField] private Transform healthRoot; // Parent object to hold heart icons

    private int maxHearts = 3; // Maximum number of hearts
    private int currentHearts; // Current number of hearts
    private readonly List<GameObject> heartObjects = new List<GameObject>(); // Cache for heart objects

    private void OnEnable()
    {
        this.MMEventStartListening<EarnCoinEvent>();
        this.MMEventStartListening<HitEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EarnCoinEvent>();
        this.MMEventStopListening<HitEvent>();
    }

    private void Start()
    {
        if (coinText == null || healthRoot == null || heartPrefab == null)
        {
            Debug.LogError("GUIHUD_Controller: Missing required references.");
            return;
        }

        // Initialize the HUD
        UpdateCoinText(0); // Start with 0 coins
        InitializeHearts(); // Create heart objects
        SetHearts(maxHearts); // Set initial hearts
    }

    public void UpdateCoinText(int coinCount)
    {
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }
    }

    private void InitializeHearts()
    {
        // Create heart objects only once
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, healthRoot);
            heartObjects.Add(heart);
        }
    }

    public void SetHearts(int hearts)
    {
        // Clamp the number of hearts to ensure it stays within the valid range
        currentHearts = Mathf.Clamp(hearts, 0, maxHearts);

        // Update the active state of each heart
        for (int i = 0; i < heartObjects.Count; i++)
        {
            heartObjects[i].SetActive(i < currentHearts);
        }
    }

    public void OnMMEvent(EarnCoinEvent eventType)
    {
        UpdateCoinText(GameManager.Instance.CoinCount);
    }

    public void OnMMEvent(HitEvent eventType)
    {
        if (currentHearts > 0)
        {
            currentHearts--;
            MMEventManager.TriggerEvent(new LoseAHeartEvent());
            heartObjects[currentHearts].SetActive(false); // Disable the last active heart
        }
        if(currentHearts == 0) 
        {
            // Handle player death or game over logic here
            Debug.Log("Player has no hearts left!");
            // You can trigger a game over event or any other logic as needed
        }
    }
}