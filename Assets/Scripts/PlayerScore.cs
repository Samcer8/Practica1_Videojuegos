using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    private int currentScore, scoreBoost;
    void Start()
    {
        currentScore = 0;
        scoreBoost = 1;
    }

    public void SetScore(int gainedPoints)
    {
        currentScore += gainedPoints * scoreBoost;
        scoreText.text = currentScore.ToString();
    }

    public void AddScoreBoost(int addedScore) 
    {
        scoreBoost = addedScore;
    }

    public void RemoveScoreBoost()
    {
        scoreBoost = 1;
    }
}
