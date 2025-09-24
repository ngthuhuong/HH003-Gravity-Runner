using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>,
    MMEventListener<EarnCoinEvent>,
    MMEventListener<HitEvent>,
    MMEventListener<DieEvent>,
    MMEventListener<LevelCompleteEvent>,
    MMEventListener<LoseAHeartEvent>
{
    [Header("---------Audio Sources---------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("--------Audio Clips---------")]
    [SerializeField] private MusicList[] musics;    
    [SerializeField] private SoundList[] soundList;

    [Serializable]
    public struct SoundList
    {
        public Sound sound;
        public AudioClip clip;
    }

    public enum MusicType
    {
        Menu,
        Gameplay
    }
    [System.Serializable]
    public struct MusicList
    {
        public MusicType type;
        public AudioClip clip;
    }

    private Dictionary<MusicType, AudioClip> musicDictionary;
    public enum Sound
    {
        Hit,
        Death,
        Score,
        Run,
        Jump,
        Click,
        LevelUp
    }

    private Dictionary<Sound, AudioClip> soundDictionary;

    protected override void Awake()
    {
        base.Awake();

        // đảm bảo AudioManager tồn tại duy nhất xuyên scene
        DontDestroyOnLoad(gameObject);
        
        musicDictionary = new Dictionary<MusicType, AudioClip>();
        foreach (var m in musics)
        {
            if (!musicDictionary.ContainsKey(m.type) && m.clip != null)
                musicDictionary.Add(m.type, m.clip);
        }

        soundDictionary = new Dictionary<Sound, AudioClip>();
        foreach (var s in soundList)
        {
            if (!soundDictionary.ContainsKey(s.sound) && s.clip != null)
                soundDictionary.Add(s.sound, s.clip);
        }
    }

    private void Start()
    {
        PlayMusic(MusicType.Menu);
    }

    private void OnEnable()
    {
        this.MMEventStartListening<EarnCoinEvent>();
        this.MMEventStartListening<HitEvent>();
        this.MMEventStartListening<DieEvent>();
        this.MMEventStartListening<LevelCompleteEvent>();
        this.MMEventStartListening<LoseAHeartEvent>();
        
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EarnCoinEvent>();
        this.MMEventStopListening<HitEvent>();
        this.MMEventStopListening<DieEvent>();
        this.MMEventStopListening<LevelCompleteEvent>();
        this.MMEventStopListening<LoseAHeartEvent>();
        
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

    public void PlayMusic(MusicType type, bool loop = true)
    {
        if (musicDictionary.TryGetValue(type, out AudioClip clip))
        {
            if (musicSource.clip == clip && musicSource.isPlaying)
                return; // tránh phát lại cùng một bài

            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }



    public void OnMMEvent(EarnCoinEvent eventType) => PlaySound(Sound.Score);
    public void OnMMEvent(HitEvent eventType) => PlaySound(Sound.Hit);
    public void OnMMEvent(DieEvent eventType) => PlaySound(Sound.Death);
    public void OnMMEvent(LevelCompleteEvent eventType) => PlaySound(Sound.LevelUp);
    public void OnMMEvent(LoseAHeartEvent eventType) => PlaySound(Sound.Hit);
}
