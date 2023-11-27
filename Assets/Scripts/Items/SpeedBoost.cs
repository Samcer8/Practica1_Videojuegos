using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Item
{
    [SerializeField]
    private FPSPlayer player;

    [SerializeField]
    private float addedSpeed;
    protected override void ExecuteAction()
    {
        StartCoroutine(TimeAction());
        base.ExecuteAction();
    }

    private IEnumerator TimeAction()
    {
        yield return null;
        player.GetComponent<PlayerMovement>().AddMoveSpeed(addedSpeed);
        yield return new WaitForSeconds(timeToExpire);
        player.GetComponent<PlayerMovement>().RemoveMoveSpeed();
    }
}
