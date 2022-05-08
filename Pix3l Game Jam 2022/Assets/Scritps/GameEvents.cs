using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance = new GameEvents();
    public event Action UpdateCoin_Ev;
    public event Action UpdateCoca_Ev;
    public event Action DialogoCapy_Ev;
    public event Action DialogoNath_Ev;
    public event Action DialogoRamona_Ev;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
    }
    public void DialogoCapy()
    {
        DialogoCapy_Ev?.Invoke();
    }
    public void DialogoNath()
    {
        DialogoNath_Ev?.Invoke();
    }
    public void DialogoRamona()
    {
        DialogoRamona_Ev?.Invoke();
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
