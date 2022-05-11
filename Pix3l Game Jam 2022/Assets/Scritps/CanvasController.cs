using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject pauseMenu;
    private void Start()
    {
        GameEvents.instance.PauseGame_Ev += PauseMenu;
        GameEvents.instance.ResumeGame_Ev += ResumePause;
    }
    public void ResumePause()
    {
        pauseMenu.SetActive(false);
    }
    public void PauseMenu()
    {
            pauseMenu.SetActive(true);
    }
}
