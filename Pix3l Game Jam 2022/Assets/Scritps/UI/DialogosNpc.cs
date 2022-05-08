using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogosNpc : MonoBehaviour,IInteractable
{
    public GameObject tmpTextGO;
    private TMP_Text myText;
    public enum Npc { Capy, Ramona, Nathaniel }
    public Npc myNpc;

    public void interact()
    {
        mostrarDialogo();
    }

    private void mostrarDialogo()
    {
        tmpTextGO.SetActive(true);
        myText = tmpTextGO.GetComponent<TMP_Text>();
        MiDialogo();
    }

    private void MiDialogo()
    {
        switch (myNpc)
        {
            case Npc.Capy:
                myText.text = "�Capy! Ay� estaba tan preocupada, por mi culpa todos est�n perdidos.";
                break;
            case Npc.Ramona:
                myText.text = "Ramona por fin te encontramos.";
                break;
            case Npc.Nathaniel:
                myText.text = "�Nathaniel, aqu� est�s! Por fin tendremos el picnic.";
                break;
        }
        StartCoroutine(anotherDialogue());
    }
    IEnumerator anotherDialogue()
    {
        yield return new WaitForSeconds(1.0f);
        switch (myNpc)
        {
            case Npc.Capy:
                myText.text = "Rel�jate. Aqu� estoy contigo linda. Vamos a buscar a los otros.";
                break;
            case Npc.Ramona:
                myText.text = "Jum� no me perd�, solo vagaba. Adem�s, ni quer�a venir al picnic.";
                break;
            case Npc.Nathaniel:
                myText.text = "�Picnic?... oh� as� que por eso vine al bosque.";
                break;
        }
        StartCoroutine(AfterSeconds());
    }

    IEnumerator AfterSeconds()
    {
        yield return new WaitForSeconds(1f);
        GameEvents.instance.ActivarDialogo(myNpc);
    }
}
