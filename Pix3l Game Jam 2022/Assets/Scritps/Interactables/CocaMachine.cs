using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocaMachine : MonoBehaviour , IInteractable
{
    [SerializeField] private GameObject visualCoke;
    public void interact()
    {
        DispenseCoke();
    }
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
   
    IEnumerator AfterSeconds()
    {
        visualCoke.SetActive(true);
        yield return new WaitForSeconds(.3f);
        GameEvents.instance.DialogoNath();
        visualCoke.SetActive(false);
    }
}
