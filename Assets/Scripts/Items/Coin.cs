using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : Item
{
    [SerializeField]
    private TMP_Text meshPro;

    protected override void ExecuteAction()
    {
        GameManager.Instance.currentCoins++;
        meshPro.text = "Coins: " + GameManager.Instance.currentCoins.ToString();
        base.ExecuteAction();
    }
}
