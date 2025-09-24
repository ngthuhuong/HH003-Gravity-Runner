using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("---------Audio Sources---------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("--------Audio Clips---------")]
    [SerializeField] private SoundList[] soundList;

    [Serializable]
    public struct SoundList
    {
        public Sound sound;         // enum thay vì string
        public AudioClip clip;      // một clip duy nhất (nếu bạn muốn nhiều thì để mảng)
    }

    public enum Sound
    {
        Music,
        Hit,
        Death,
        Score,
        Run,
        Jump,
        Click
    }

    private Dictionary<Sound, AudioClip> soundDictionary;

    private void Awake()
    {
        soundDictionary = new Dictionary<Sound, AudioClip>();
        foreach (var s in soundList)
        {
            if (!soundDictionary.ContainsKey(s.sound) && s.clip != null)
                soundDictionary.Add(s.sound, s.clip);
        }
    }

    private void Start()
    {
        PlayMusic(Sound.Music);
    }

    public void PlaySound(Sound sound)
    {
        if (soundDictionary.TryGetValue(sound, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound {sound} chưa được gán clip trong AudioManager!");
        }
    }

    public void PlayMusic(Sound sound, bool loop = true)
    {
        if (soundDictionary.TryGetValue(sound, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }
}