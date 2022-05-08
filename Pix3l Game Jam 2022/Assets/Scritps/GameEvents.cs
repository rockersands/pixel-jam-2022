using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance = new GameEvents();
    public event Action UpdateCoin_Ev;
    public event Action UpdateCoca_Ev;
    public event Action<DialogosNpc.Npc> ActivarDialogo_Ev;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
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
