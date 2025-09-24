using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void PlaySound(AudioManager.Sound sound)
    {
        AudioManager.Instance.PlaySound(sound);
    }
}
