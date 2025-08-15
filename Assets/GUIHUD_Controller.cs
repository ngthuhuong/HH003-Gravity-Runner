using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIHUD_Controller : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Reference to the Text component for coin count
    public List<Image> heartImages; // List of Image components for hearts
    //public Sprite fullHeart; // Sprite for a full heart
    //public Sprite emptyHeart; // Sprite for an empty heart

    private int maxHearts = 3; // Maximum number of hearts
    private int currentHearts; // Current number of hearts

    void Start()
    {
        // Initialize the HUD
        UpdateCoinText(0); // Start with 0 coins
      //  SetHearts(maxHearts); // Start with full hearts
    }

    public void UpdateCoinText(int coinCount)
    {
        // Update the coin count display
        coinText.text = $"Coins: {coinCount}";
    }

  
    public void SetCoinText(int text)
    {
        if (coinText != null)
        {
            coinText.text = text.ToString(); // Update the coin text
        }
    }
}