using UnityEngine;
using UnityEngine.UI;

public class LevelUIBinder : MonoBehaviour
{
    void Start()
    {
        // Find all buttons in the scene
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            // Remove existing listeners to avoid duplicates
           // button.onClick.RemoveAllListeners();

            // Add a listener to play the click sound
            button.onClick.AddListener(() => PlayClickSound());
        }
    }

    private void PlayClickSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.Click);
    }
}