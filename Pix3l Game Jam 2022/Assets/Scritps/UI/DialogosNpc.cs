using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogosNpc : MonoBehaviour
{
    public GameObject tmpTextGO,canvasGo;
    private TMP_Text myText;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] private Sprite Capy, Ramona, Nathaniel;
    public enum Npc { Capy, Ramona, Nathaniel }
    public Npc myNpc;
    private void Start()
    {
        myText = tmpTextGO.GetComponent<TMP_Text>();
        switch (myNpc)
        {
            case Npc.Capy:
                mySpriteRenderer.sprite = Capy;
                break;
            case Npc.Ramona:
                mySpriteRenderer.sprite = Ramona;
                break;
            case Npc.Nathaniel:
                mySpriteRenderer.sprite = Nathaniel;
                break;
            default:
                break;
        }
    }

    public void MostrarDialogo()
    {
        canvasGo.SetActive(true);
        MiDialogo();
    }

    private void MiDialogo()
    {
        switch (myNpc)
        {
            case Npc.Capy:
                AudioController.PlayVoices(AudioController.Voice.capyTalk);
                AudioController.PlayContinuosSound(AudioController.ContinuosSound.PlayerRunning);
                myText.text = "Relájate. Aquí estoy contigo linda.\n Vamos a buscar a los otros.";
                break;
            case Npc.Ramona:
                AudioController.PlayVoices(AudioController.Voice.ramonaTalk);
                myText.text = "Jum… no me perdí, solo vagaba. Además,\n ni quería venir al picnic.";
                break;
            case Npc.Nathaniel:
                AudioController.PlayVoices(AudioController.Voice.nathTalk);
                myText.text = "¿Picnic?... oh… así que por\n eso vine al bosque.";
                break;
        }
        StartCoroutine(AnotherDialogue());
    }
    IEnumerator AnotherDialogue()
    {
        yield return new WaitForSeconds(5.0f);
        canvasGo.SetActive(false);
        GameEvents.instance.ActivarDialogo(myNpc);
    }

}
