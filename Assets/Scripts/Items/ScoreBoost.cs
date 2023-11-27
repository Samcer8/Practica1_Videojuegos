using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoost : Item
{
    [SerializeField]
    private PlayerScore score;
    [SerializeField]
    private int scoreBoost;
    protected override void ExecuteAction()
    {
        //player.addJumpBoost();
        StartCoroutine(TimeAction());
        //player.removeJumpBoost();
        base.ExecuteAction();
    }

    private IEnumerator TimeAction()
    {
        yield return null;

        score.AddScoreBoost(scoreBoost);
        yield return new WaitForSeconds(timeToExpire);
        score.RemoveScoreBoost();
    }
}
