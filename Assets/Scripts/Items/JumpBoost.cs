using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : Item
{
    [SerializeField]
    private FPSPlayer player;

    [SerializeField]
    private float addedForce;
    protected override void ExecuteAction()
    {
        StartCoroutine(TimeAction());
        base.ExecuteAction();
    }

    private IEnumerator TimeAction()
    {
        yield return null;
        player.GetComponent<PlayerMovement>().AddJumpForce(addedForce);
        yield return new WaitForSeconds(timeToExpire);
        player.GetComponent<PlayerMovement>().RemoveJumpForce();
    }
}
