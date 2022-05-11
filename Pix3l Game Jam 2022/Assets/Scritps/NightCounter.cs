using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NightCounter : MonoBehaviour
{
    [SerializeField] private GameObject gameOverStuff;
    [SerializeField] private Image progressBarNight;
    [SerializeField] private Image blackOut;
    public float secondsTillGameOver,originalTimeStart;
    private bool gamePause,gameWasPaused;
    private void Start()
    {

        GameEvents.instance.GameStart_Ev += StartProgressBar;
        GameEvents.instance.PauseGame_Ev += StopTimer;
        GameEvents.instance.ResumeGame_Ev += ResumeTimer;
    }
    [ContextMenu("Start Progressbar")]
    public void StartProgressBar()
    {
        Debug.Log("start progress");
        StartCoroutine(ProgressBar());
    }
    #region progressBar
    IEnumerator ProgressBar()
    {
        #region variables
        float timeStart;
        Color tempColor = blackOut.color;
        if(!gameWasPaused)
            timeStart = Time.time;
        else { timeStart = originalTimeStart; }
        float timeTillGameEnd = Time.time + secondsTillGameOver;
        float onePercent = timeTillGameEnd / 100;
        if(!gameWasPaused)
            progressBarNight.fillAmount = 0;
        #endregion
        #region clockworks 
        while (progressBarNight.fillAmount < 1)
        {
            progressBarNight.fillAmount = ((Time.time - timeStart) / onePercent) / 100;
            tempColor.a = progressBarNight.fillAmount;
            blackOut.color = tempColor;
            #region gamePause
            if (gamePause)
            {
                if(!gameWasPaused)
                    originalTimeStart = timeStart;
                gameWasPaused = true;
                secondsTillGameOver = timeTillGameEnd - Time.time;
                yield return null;
                break;
            }
            #endregion
            yield return null;
        }
        if(progressBarNight.fillAmount >= 1)
        {
            gameOverStuff.SetActive(true);
        }
        #endregion
        yield return null;
    }
    #endregion
    #region stopTimer
    [ContextMenu("StopTimer")]
    public void StopTimer()
    {
        gamePause = true;
    }
    #endregion
    [ContextMenu("ResumeTimer")]
    public void ResumeTimer()
    {
        gamePause = false;
        StartCoroutine(ProgressBar());
    }
}
