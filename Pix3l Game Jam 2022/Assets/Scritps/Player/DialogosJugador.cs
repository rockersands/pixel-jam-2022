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
        GameEvents.instance.ActivarDialogo_Ev += DialogoNath;
    }
    private void DialogoNath(DialogosNpc.Npc npc)
    {
        switch (npc)
        {
            case DialogosNpc.Npc.estrella:
                break;
            case DialogosNpc.Npc.mar:
                break;
            case DialogosNpc.Npc.samantha:
                break;
            default:
                break;
        }
    }
}
