using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogosJugador : MonoBehaviour
{
    [SerializeField]private GameObject tmpTextObject,CanvasGo;
    private TMP_Text playerDialogue;
    private void Start()
    {
        playerDialogue = tmpTextObject.GetComponent<TMP_Text>();
        GameEvents.instance.ActivarDialogo_Ev += ActivarDialogo;
    }
    private void ActivarDialogo(DialogosNpc.Npc npc)
    {
        CanvasGo.SetActive(true);
        AudioController.PlayVoices(AudioController.Voice.playerTalk);
        switch (npc)
        {
            case DialogosNpc.Npc.Capy:
                playerDialogue.text = "¡Capy! Ay… estaba tan preocupada, \n por mi culpa todos están perdidos.";
                break;
            case DialogosNpc.Npc.Ramona:
                playerDialogue.text = "Ramona por fin te encontramos.";
                break;
            case DialogosNpc.Npc.Nathaniel:
                playerDialogue.text = "¡Nathaniel, aquí estás! \n Por fin tendremos el picnic.";
                break;
            default:
                break;
        }
        StartCoroutine(TurnOffTextBubble());
    }

    IEnumerator TurnOffTextBubble()
    {
        yield return new WaitForSeconds(5f);
        CanvasGo.SetActive(false);
    }
}
