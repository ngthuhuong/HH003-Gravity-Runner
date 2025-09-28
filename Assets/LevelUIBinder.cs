using UnityEngine;
using UnityEngine.UI;

public class LevelUIBinder : MonoBehaviour
{
    void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayClickSound());
        }
    }

    private void PlayClickSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.Click);
    }
}