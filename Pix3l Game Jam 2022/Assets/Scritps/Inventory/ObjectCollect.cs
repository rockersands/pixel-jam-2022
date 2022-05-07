using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollect : MonoBehaviour
{
    public string objTag;
    public string cocaTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(objTag))
        {
            FindObjectOfType<Inventory>().countCoins++; 
            // agregar sonido de recoleccion de una moneda
            if(FindObjectOfType<Inventory>().countCoins == FindObjectOfType<Inventory>().maxCoins)
            {
                FindObjectOfType<Inventory>().getCoca = true;
            }
        }
        else if (collision.tag.Equals(cocaTag))
        {
            if (FindObjectOfType<Inventory>().getCoca)
            {
                FindObjectOfType<Inventory>().cocaCount++; //poner en la caja de texto
                FindObjectOfType<Inventory>().txtCocaCount.text = "" + FindObjectOfType<Inventory>().cocaCount;
                FindObjectOfType<Inventory>().getCoca = false;
            }
        }
    }
}
