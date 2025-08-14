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