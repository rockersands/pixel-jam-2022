using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayThisAudio : MonoBehaviour
{
    public AudioClip select;
    public AudioSource audioSourcce;
   public void selectSound()
    {
        audioSourcce.PlayOneShot(select);
    }
}
