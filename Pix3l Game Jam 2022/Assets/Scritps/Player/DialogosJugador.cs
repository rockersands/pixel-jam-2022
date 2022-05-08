using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogosJugador : MonoBehaviour
{
    [SerializeField]private GameObject tmpTextObject;
    private TMP_Text playerDialogue;
    private void Start()
    {
        playerDialogue = tmpTextObject.GetComponent<TMP_Text>();
        GameEvents.instance.ActivarDialogo_Ev += ActivarDialogo;
    }
    private void ActivarDialogo(DialogosNpc.Npc npc)
    {
        tmpTextObject.SetActive(true);
        switch (npc)
        {
            case DialogosNpc.Npc.Capy:
                playerDialogue.text = "Relájate. Aquí estoy contigo linda.\n Vamos a buscar a los otros.";
                break;
            case DialogosNpc.Npc.Ramona:
                playerDialogue.text = "Jum… no me perdí, solo vagaba. Además,\n ni quería venir al picnic.";
                break;
            case DialogosNpc.Npc.Nathaniel:
                playerDialogue.text = "¿Picnic?... oh… así que por\n eso vine al bosque.";
                break;
            default:
                break;
        }
        StartCoroutine(TurnOffTextBubble());
    }

    IEnumerator TurnOffTextBubble()
    {
        yield return new WaitForSeconds(3f);
        tmpTextObject.SetActive(false);
    }
}
