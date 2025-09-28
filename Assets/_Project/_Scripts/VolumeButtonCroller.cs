using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public Image volumeImage;   
    public TextMeshProUGUI volumeText; 
    public Sprite unmuteSprite;  
    public Sprite muteSprite;     

    private bool isMuted = false; 

    private void Start()
    {
        
    }


    public void ToggleMute()
    {
        isMuted = !isMuted; 
        if (isMuted)
        {
            volumeImage.sprite = muteSprite;
            volumeText.text = "Unmute";
            AudioListener.volume = 0f;  
        }
        else
        {
            volumeImage.sprite = unmuteSprite;
            volumeText.text = "Mute";
            AudioListener.volume = 1f;  
        }
    }
}