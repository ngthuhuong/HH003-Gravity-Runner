using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class RewardManager : MonoBehaviour,MMEventListener<GetBoxEvent>
{
    public List<Reward> rewards;

    public Reward GetRandomReward()
    {
        int totalWeight = 0;
        foreach (var r in rewards) totalWeight += r.weight;

        int randomValue = Random.Range(0, totalWeight);
        int currentSum = 0;

        foreach (var r in rewards)
        {
            currentSum += r.weight;
            if (randomValue < currentSum)
            {
                return r;
            }
        }
        return null;
    } 
    
    private void OnEnable()
    {
        this.MMEventStartListening<GetBoxEvent>();
    }
    private void OnDisable()
    {
        this.MMEventStopListening<GetBoxEvent>();
    }

    public void OnMMEvent(GetBoxEvent eventType)
    {
        Reward reward = GetRandomReward();
        Debug.Log($"Reward: {reward.type} - Amount: {reward.amount} - Weight: {reward.weight}");
        if (reward.type == RewardType.Coin)
        {
            MMEventManager.TriggerEvent(new EarnCoinEvent(reward.amount));
        }else if (reward.type == RewardType.Heart)
        {
            MMEventManager.TriggerEvent(new GetAHeart());
        }
        MMEventManager.TriggerEvent(new EarnRewardEvent(reward));
    }
}