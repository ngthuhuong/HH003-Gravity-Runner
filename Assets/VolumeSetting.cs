using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer; // Reference to the AudioMixer
    private const string parameterName = "Music"; // Name of the exposed parameter in the AudioMixer
    private void Start()
    {
       
    }

    public void SetVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat(parameterName, Mathf.Log10(volume) * 20); 
    }
    
}