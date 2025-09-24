using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip clickClip;

    void Start()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(() => PlayClickSound());
        }
    }

    private void PlayClickSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.Click);
    }
}