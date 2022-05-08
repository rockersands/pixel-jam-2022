using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogosNpc : MonoBehaviour,IInteractable
{
    public GameObject tmpTextGO;
    private TMP_Text myText;
    public enum Npc { estrella,mar,samantha}
    public Npc myNpc;

    public void interact()
    {
        myText = tmpTextGO.GetComponent<TMP_Text>();
    }
    private void MiDialogo()
    {
        switch (myNpc)
        {
            case Npc.estrella:
                break;
            case Npc.mar:
                break;
            case Npc.samantha:
                break;
            default:
                break;
        }

    }
    IEnumerator AfterSeconds()
    {
        yield return new WaitForSeconds(1f);
        GameEvents.instance.ActivarDialogo(myNpc);
    }
}
