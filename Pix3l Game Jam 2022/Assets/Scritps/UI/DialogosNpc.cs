using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogosNpc : MonoBehaviour
{
    public GameObject tmpTextGO;
    private TMP_Text myText;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeReference] Sprite Capy, Ramona, Nathaniel;
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
        tmpTextGO.SetActive(true);
        MiDialogo();
    }

    private void MiDialogo()
    {
        switch (myNpc)
        {
            case Npc.Capy:
                myText.text = "¡Capy! Ay… estaba tan preocupada, \n por mi culpa todos están perdidos.";
                break;
            case Npc.Ramona:
                myText.text = "Ramona por fin te encontramos.";
                break;
            case Npc.Nathaniel:
                myText.text = "¡Nathaniel, aquí estás! \n Por fin tendremos el picnic.";
                break;
        }
        StartCoroutine(AnotherDialogue());
    }
    IEnumerator AnotherDialogue()
    {
        yield return new WaitForSeconds(1.0f);
        GameEvents.instance.ActivarDialogo(myNpc);
        yield return new WaitForSeconds(2.0f);
        tmpTextGO.SetActive(false);
    }

}
