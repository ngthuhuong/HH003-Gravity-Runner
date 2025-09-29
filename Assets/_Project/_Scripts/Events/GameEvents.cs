using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public struct HitEvent
{
    
}
public struct EarnCoinEvent
{
    public int coinCount;

    public EarnCoinEvent(int count)
    {
        coinCount = count;
    }
}
public struct LoseAHeartEvent
{

}
public struct ResumeGameEvent
{
    
}
public struct DieEvent
{
   
}

public struct GetBoxEvent
{
  
}

public struct GetAHeart
{
    
}
public struct EarnRewardEvent
{
    public Reward reward;

    public EarnRewardEvent(Reward r)
    {
        reward = r;
    }
}

public struct LevelCompleteEvent
{
    
}

public struct LoadedData
{
    
}
public struct PauseGameEvent
{
    public static PauseGameEvent Trigger()
    {
        return new PauseGameEvent();
    }
}

public struct NoMap
{
    
}