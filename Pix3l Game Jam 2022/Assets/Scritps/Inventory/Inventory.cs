using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventory : MonoBehaviour
{
    private void Start()
    {
        GameEvents.instance.UpdateCoin_Ev += UpdateCoinText;
        GameEvents.instance.UpdateCoca_Ev += UpdateCocaText;
    }
    public TMP_Text txtCocaCount,txtCoinsCount;
    [SerializeField] private int maxCoins;
    public static int currentCoins, cocaCount;

    public bool getCoca= false;
    private void UpdateCoinText()
    {
        txtCoinsCount.text = $"X {currentCoins}";
    }
    private void UpdateCocaText()
    {
        txtCocaCount.text = $"X {cocaCount}";
    }
}
