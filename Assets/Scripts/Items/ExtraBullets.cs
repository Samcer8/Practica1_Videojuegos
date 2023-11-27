using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtraBullets : Item
{
    [SerializeField]
    private int extraBullets;

    protected override void ExecuteAction()
    {
        GameManager.Instance.addAmmo(extraBullets);
        base.ExecuteAction();
    }
}
