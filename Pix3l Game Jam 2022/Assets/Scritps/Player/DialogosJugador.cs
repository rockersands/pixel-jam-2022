using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogosJugador : MonoBehaviour
{
    private GameObject tmpTextObject;
    private TMP_Text playerDialogue;
    private void Start()
    {
        playerDialogue = tmpTextObject.GetComponent<TMP_Text>();
        GameEvents.instance.DialogoNath_Ev += DialogoNath;
    }
    private void DialogoNath()
    {
        string diagNath = "¿Picnic?... oh… así que por eso vine al bosque.";
        tmpTextObject.SetActive(true);
        playerDialogue.text = diagNath;
    }
    private void DialogoRamona()
    {
        string diagNath = "¿Picnic?... oh… así que por eso vine al bosque.";

    }
    private void DialogoCapy()
    {
        string diagNath = "¿Picnic?... oh… así que por eso vine al bosque.";

    }
}
