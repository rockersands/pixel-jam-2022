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
    public AudioMixerGroup voiceMixerGroup;
    [Header("SoundClips")]
    public VoiceAudioClip[] VoiceAudioClipArray;
    public SfxAudioClip[] SfxAudioClipArray;
    public SongAudioClip[] SongAudioClipArray;
    public ContinuosAudioClip[] ContinuosAudioClipArray;
    private void Start()
    {
        AudioController.myAudioSourcesContinuos.Clear();
        AudioController.myAudioSourcesSfx.Clear();
        AudioController.myAudioSourcesVoices.Clear();
        AudioController.myAudioSourcesSongs.Clear();
        AudioController.sfxParent = null;
        AudioController.SongsParent = null;
        AudioController.ContinuosParent = null;
        AudioController.voicesParent = null;

        AudioController.voiceMixerGroup = voiceMixerGroup;
        AudioController.sfxMixerGroup = sfxMixerGroup;
        AudioController.songsMixerGroup = songsMixerGroup;
    }
    [System.Serializable]
    public class VoiceAudioClip
    {
        public AudioController.Voice sound;
        public AudioClip audioClip;
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
