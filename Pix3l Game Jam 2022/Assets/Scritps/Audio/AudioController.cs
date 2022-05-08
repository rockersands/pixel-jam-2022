using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public static class AudioController
{
    #region variables
    public static AudioMixerGroup sfxMixerGroup, songsMixerGroup;
    private static List<AudioSource> myAudioSourcesContinuos = new List<AudioSource>();
    private static List<AudioSource> myAudioSourcesSfx = new List<AudioSource>();
    private static List<AudioSource> myAudioSourcesSongs = new List<AudioSource>();
    private static GameObject sfxParent, SongsParent, ContinuosParent;
    #endregion
    #region Enums
    #region ContinuosSound
    public enum ContinuosSound
    {
        PlayerRunning
    }
    #endregion
    #region Sfx
    public enum Sfx
    {
        playerHit, playerInteract, playerTalk,
        playerDie
    }
#endregion
    #region Songs
    public enum Songs
    {
        songFirstTownNight, songFirstTownDay, songSecondTown,
        songSecondTownNight, songFight
    }
    #endregion
    #endregion
    #region playingRequests
    #region ContinuosSound
    #region Play
    public static void PlayContinuosSound(ContinuosSound sound)
    {
        bool foundAvailableAudioSource = false;

        if (ContinuosParent == null)
        {
            ContinuosParent = new GameObject("ContinuosAudSourParent");
        }
        for (int i = 0; i < myAudioSourcesContinuos.Count; i++)
        {
            if (!myAudioSourcesContinuos[i].isPlaying)
            {
                foundAvailableAudioSource = true;
                myAudioSourcesContinuos[i].clip = null;
                myAudioSourcesContinuos[i].clip = GetContinuosAudioClip(sound);
                myAudioSourcesContinuos[i].Play();
            }
        }
        if (!foundAvailableAudioSource)
        {
            GameObject soundGameObject = new GameObject("ContinuosSound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            soundGameObject.transform.parent = ContinuosParent.transform;
            myAudioSourcesContinuos.Add(audioSource);
            audioSource.outputAudioMixerGroup = sfxMixerGroup;
            audioSource.clip = null;
            audioSource.clip = GetContinuosAudioClip(sound);
            audioSource.Play();
        }
    }
    #endregion
    #region StopContinuosSound
    public static void StopContinuosSound(ContinuosSound sound)
    {
        bool StoppedCorrectAudio = false;
        for (int i = 0; i<myAudioSourcesContinuos.Count; i++)
        {
            if (myAudioSourcesContinuos[i].isPlaying)
            {
                if(myAudioSourcesContinuos[i].clip == GetContinuosAudioClip(sound))
                {
                    myAudioSourcesContinuos[i].Stop();
                    StoppedCorrectAudio = true;
                }
            }
        }
        if (!StoppedCorrectAudio)
            Debug.LogError("Couldnt find audio to stop");
    }
    #endregion
    #endregion
    #region playSongs
    public static void PlaySong(Songs sound)
    {
        //subir volumen musica nueva tarea
        if (SongsParent == null)
        {
            SongsParent = new GameObject("SongsAudSourParent");
        }
        bool foundPreviosSongToMute = false,foundAvailableAudioSource = false;
        if (myAudioSourcesSongs.Count > 0)
        {
            for (int i = 0; i < myAudioSourcesSongs.Count; i++)
            {
               if(myAudioSourcesSongs[i].isPlaying)
               {
                   GraduallyLoweringVolume(myAudioSourcesSongs[i],1);
                    foundPreviosSongToMute = true;
               } 
            }
        }
        if(foundPreviosSongToMute)
        {
            for (int i = 0; i < myAudioSourcesSongs.Count; i++)
            {
                if (!myAudioSourcesSongs[i].isPlaying)
                {
                    foundAvailableAudioSource = true;
                    myAudioSourcesSongs[i].PlayOneShot(GetSongAudioClip(sound));
                }
            }
        }
        if(!foundAvailableAudioSource)
        {
            GameObject soundGameObject = new GameObject("Song");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            myAudioSourcesSongs.Add(audioSource);
            audioSource.outputAudioMixerGroup = songsMixerGroup;
            soundGameObject.transform.parent = SongsParent.transform;
            audioSource.PlayOneShot(GetSongAudioClip(sound));
        }
    }
    #endregion
    #region PlaySfx
    public static void PlaySfx(Sfx sound)
    {
        if (sfxParent == null)
        {
            sfxParent = new GameObject("SFXAudSourParent");
        }
        bool foundAvailableAudioSource = false;
        for (int i = 0; i < myAudioSourcesSfx.Count; i++)
        {
            if (!myAudioSourcesSfx[i].isPlaying)
            {
                foundAvailableAudioSource = true;
                myAudioSourcesSfx[i].PlayOneShot(GetSFXAudioClip(sound));
            }
        }
        if (!foundAvailableAudioSource)
        {
            GameObject soundGameObject = new GameObject("Sfx");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            soundGameObject.transform.parent = sfxParent.transform;
            audioSource.outputAudioMixerGroup = sfxMixerGroup;
            myAudioSourcesSfx.Add(audioSource);
            audioSource.PlayOneShot(GetSFXAudioClip(sound));
        }
    }
    #endregion
    #endregion
    #region GettingAudioclips
    #region GetSongAudioClip
    private static AudioClip GetContinuosAudioClip(ContinuosSound sound)
    {
        foreach (GameAssets.ContinuosAudioClip soundAudioClip in GameAssets.Instance.ContinuosAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError($"Sound {sound} not found");
        return null;
    }
    #endregion
    #region GetSongAudioClip
    private static AudioClip GetSongAudioClip(Songs sound)
    {
        foreach (GameAssets.SongAudioClip soundAudioClip in GameAssets.Instance.SongAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError($"Sound {sound} not found");
        return null;
    }
    #endregion
    #region GetSFXAudioClip
    private static AudioClip GetSFXAudioClip(Sfx sound)
    {
        foreach (GameAssets.SfxAudioClip soundAudioClip in GameAssets.Instance.SfxAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError($"Sound {sound} not found");
        return null;
    }
    #endregion
    #endregion
    #region GraduallyLoweringVolume
    private static void GraduallyLoweringVolume( AudioSource audioSource, float fadeTime)
    {
        while(audioSource.isPlaying)
        {
            audioSource.volume = Mathf.Lerp(1, 0, fadeTime);
            if(audioSource.volume < .1)
            {
                audioSource.Stop();
                audioSource.volume = 1;
                break;
            }
        }
    }
    #endregion
}