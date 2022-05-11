using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGAME : MonoBehaviour
{
    public GameObject endGO;

    void Start()
    {
        GameEvents.instance.FinishedGame_Ev += EndGame;
    }
    [ContextMenu("endGame")]
    public void EndGame()
    {
        StartCoroutine(waitForEndgame());
    }
    IEnumerator waitForEndgame()
    {
        yield return new WaitForSeconds(10f);
        endGO.SetActive(true);
    }
}
