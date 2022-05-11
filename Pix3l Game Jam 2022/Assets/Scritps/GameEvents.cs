using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    private bool pauseIsntResumed;
    public event Action UpdateCoin_Ev; 
    public event Action FinishedGame_Ev;
    public event Action GameStart_Ev;
    public event Action UpdateCoca_Ev;
    public event Action ResumeGame_Ev;
    public event Action PauseGame_Ev;
    public event Action<DialogosNpc.Npc> ActivarDialogo_Ev;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else { instance = new GameEvents(); }
    }
    public void ResumeGame()
    {
        ResumeGame_Ev?.Invoke();
        pauseIsntResumed = false;
    }
    public void PauseGame()
    {
        if (!pauseIsntResumed)
            PauseGame_Ev?.Invoke();
        else if (pauseIsntResumed)
            ResumeGame();
        pauseIsntResumed = true;
    }
    public void FinishedGame()
    {
        FinishedGame_Ev?.Invoke();
    }
    public void GameStart()
    {
        GameStart_Ev?.Invoke();
    }
    public void ActivarDialogo(DialogosNpc.Npc npc)
    {
        ActivarDialogo_Ev?.Invoke(npc);
    }
    public void UpdateCoin()
    {
        UpdateCoin_Ev?.Invoke();
    }

    public void UpdateCoca()
    {
        UpdateCoca_Ev?.Invoke();
    }
}
