using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocaMachine : MonoBehaviour , IInteractable
{
    [SerializeField] private GameObject visualCoke;
    private void DispenseCoke()
    {
        if(Inventory.currentCoins >= 3)
        {
            Debug.Log("DispenseCoke");
            Inventory.currentCoins -= 3;
            Inventory.cocaCount += 1;
            StartCoroutine(AfterSeconds());
            GameEvents.instance.UpdateCoca();
            GameEvents.instance.UpdateCoin();
        }
    }
    public void interact()
    {
        DispenseCoke();
    }
    IEnumerator AfterSeconds()
    {
        visualCoke.SetActive(true);
        yield return new WaitForSeconds(.3f);
        visualCoke.SetActive(false);
    }
}
