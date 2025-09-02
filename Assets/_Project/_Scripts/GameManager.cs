using MoreMountains.Tools;
using UnityEngine;

public class GameManager : Singleton<GameManager>, MMEventListener<EarnCoinEvent>
{
    
    private int coinCount = 0;
    public int CoinCount
    {
        get { return coinCount; }
        private set { coinCount = value; }
    }

    private GUIHUD_Controller GUIHUD;

    
    private void OnEnable()
    {
        this.MMEventStartListening<EarnCoinEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EarnCoinEvent>();
    }

    void Start()
    {
        GUIHUD = FindObjectOfType<GUIHUD_Controller>();
    }

    public void OnMMEvent(EarnCoinEvent eventType)
    {
        CoinCount += eventType.coinCount;
    }
}