using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GameManager : Singleton<GameManager>, MMEventListener<EarnCoinEvent>, MMEventListener<LevelCompleteEvent>
{
    private int coinCount = 0;
    public int CoinCount => coinCount;

    private const string CoinKey = "CoinCount";
    private const string LevelKey = "Level";

    public int Level { get; private set; } = 1;

    private GUIHUD_Controller GUIHUD;
    [SerializeField] private List<GameObject> Maps = new List<GameObject>();

    private GameObject currentMapInstance; // giữ tham chiếu map đang chạy

    private void OnEnable()
    {
        this.MMEventStartListening<EarnCoinEvent>();
        this.MMEventStartListening<LevelCompleteEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EarnCoinEvent>();
        this.MMEventStopListening<LevelCompleteEvent>();
    }

    private void Start()
    {
        GUIHUD = FindObjectOfType<GUIHUD_Controller>();
        LoadData();
    }

    public void SetCoin(int newCoin)
    {
        coinCount = newCoin;
        SaveData();
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        SaveData();
    }

    public void NextLevel( )
    {
        Level+=1;
        SaveData();
        Debug.Log("Level set to: " + Level);
    }

    public void OnMMEvent(EarnCoinEvent eventType)
    {
        AddCoin(eventType.coinCount);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(CoinKey, coinCount);
        PlayerPrefs.SetInt(LevelKey, Level);
        PlayerPrefs.Save();
        Debug.Log($"Data saved: Coin = {coinCount}, Level = {Level}");
    }

    private void LoadData()
    {
        if (!PlayerPrefs.HasKey(CoinKey))
        {
            PlayerPrefs.SetInt(CoinKey, 0);
            PlayerPrefs.SetInt(LevelKey, 1);
            PlayerPrefs.Save();
            Debug.Log("Initialized data: Coin = 0, Level = 1");
        }

        coinCount = PlayerPrefs.GetInt(CoinKey);
        Level = PlayerPrefs.GetInt(LevelKey);
        Debug.Log($"Data loaded: Coin = {coinCount}, Level = {Level}");
    }

    public void PlayALevel()
    {
        Debug.Log("Playing level");

        if (currentMapInstance != null)
        {
            Destroy(currentMapInstance);
        }

        if (Level <= Maps.Count && Maps[Level - 1] != null)
        {
            currentMapInstance = Instantiate(Maps[Level - 1], Vector3.zero, Quaternion.identity);
            Debug.Log($"Instantiated map for level {Level}");
        }
        else
        {
            Debug.Log("No level found or map is null.");
        }
    }

    public void OnMMEvent(LevelCompleteEvent eventType)
    {
        NextLevel();
    }
}
