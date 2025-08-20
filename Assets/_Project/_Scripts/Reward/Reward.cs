using UnityEngine;

[System.Serializable]
public class Reward
{
    public string rewardName;   // Tên hiển thị ("100 Coins", "1 Heart")
    public RewardType type;     // Enum: Coin, Heart, Item, ...
    public int amount;          // Số lượng (100 coin, 1 heart, ...)
    public Sprite icon;         // Ảnh hiện trên GUI
    public int weight;          // Trọng số (tỷ lệ rơi)
}

public enum RewardType
{
    Coin,
    Heart,
    Item
}