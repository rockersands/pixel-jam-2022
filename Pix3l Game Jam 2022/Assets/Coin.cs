using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory.currentCoins += 1;
        GameEvents.instance.UpdateCoin();
        Destroy(gameObject);
    }

}
