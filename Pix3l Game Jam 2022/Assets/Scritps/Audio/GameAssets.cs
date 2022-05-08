using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAssets : MonoBehaviour
{
    #region singleton
    public static GameAssets Instance;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    #endregion
    [Header("MixerGroups")]
    public AudioMixerGroup sfxMixerGroup;
    public AudioMixerGroup songsMixerGroup;
    [Header("SoundClips")]
    public SfxAudioClip[] SfxAudioClipArray;
    public SongAudioClip[] SongAudioClipArray;
    public ContinuosAudioClip[] ContinuosAudioClipArray;
    private void Start()
    {
        AudioController.sfxMixerGroup = sfxMixerGroup;
        AudioController.songsMixerGroup = songsMixerGroup;
    }
    [System.Serializable]
    public class SfxAudioClip
    {
        public AudioController.Sfx sound;
        public AudioClip audioClip;
    }
    [System.Serializable]
    public class SongAudioClip
    {
        public AudioController.Songs sound;
        public AudioClip audioClip;
    }
    [System.Serializable]
    public class ContinuosAudioClip
    {
        public AudioController.ContinuosSound sound;
        public AudioClip audioClip;
    }
    
}
