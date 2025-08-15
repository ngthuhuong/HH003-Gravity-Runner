using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GameManager : MonoBehaviour, MMEventListener<HitEvent>,MMEventListener<EarnCoinEvent>
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    
    
    
    private int coinCount = 0; // Total coins collected
    public int CoinCount
    {
        get { return coinCount; }
        private set
        {
            coinCount = value;
            // Optionally, you can trigger an event here if needed
            // MMEventManager.TriggerEvent(new CoinCountChangedEvent(coinCount));
        }
    }

    private GUIHUD_Controller GUIHUD;
    
    private void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        // Register to listen to events
        this.MMEventStartListening<HitEvent>();
        this.MMEventStartListening<EarnCoinEvent>();
    }
    void Start()
    {
        GUIHUD = new GUIHUD_Controller();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnMMEvent(EarnCoinEvent eventType)
    {
        CoinCount += eventType.coinCount;
        GUIHUD.SetCoinText(CoinCount);
    }

    public void OnMMEvent(HitEvent eventType)
    {
        throw new System.NotImplementedException();
    }
}
