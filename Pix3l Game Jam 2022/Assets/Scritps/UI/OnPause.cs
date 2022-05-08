using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPause : MonoBehaviour
{
    public bool isPause;
    public GameObject panelPause;

    public void ShowPause()
    {
        isPause = true;
        panelPause.SetActive(true);
    }

    public void HidePause()
    {
        panelPause.SetActive(true);
    }

    public void inPlayerMovement()
    {
        if (!isPause)
        {
            
        }
    }
}
