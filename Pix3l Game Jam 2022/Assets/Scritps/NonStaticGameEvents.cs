using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonStaticGameEvents : MonoBehaviour
{
    public void GameStarted()
    {
        GameEvents.instance.GameStart();
    }
    public void Pause()
    {
        GameEvents.instance.PauseGame();
    }
    public void Resume()
    {
        GameEvents.instance.ResumeGame();
    }
}
