using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTicItem : Item
{
    [SerializeField] private float totalHealth, totalTicks, timeBetweenTicks;
    [SerializeField] private int recoverHealth;
    protected override void ExecuteAction()
    {
        StartCoroutine(ExecuteTickAction());
        base.ExecuteAction();
    }

    private IEnumerator ExecuteTickAction()
    {
        yield return null;

        var currentTick = 0;

        while(currentTick < totalTicks)
        {
            //player.AddHealth(totalHealth/totalTicks);
            GameManager.Instance.addHealth(recoverHealth);
            currentTick++;
            yield return new WaitForSeconds(timeBetweenTicks);
        }
    }
}
