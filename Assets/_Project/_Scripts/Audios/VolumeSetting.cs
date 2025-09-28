using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer; // Reference to the AudioMixer
    private float volume; 
    private const string parameterName = "Music"; // Name of the exposed parameter in the AudioMixer
    private void Start()
    {
        volume = AudioManager.Instance.Volume;
        volumeSlider.value = volume;
    }

    public void SetVolume()
    {
        volume = volumeSlider.value;
        AudioManager.Instance.Volume = volume;
        audioMixer.SetFloat(parameterName, Mathf.Log10(volume) * 20); 
    }
    
}