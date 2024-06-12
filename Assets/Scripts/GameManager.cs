using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //singleton

    public GameUI gameUI;
    public int scorePlayer1, scorePlayer2;    
    public Action onReset;  //delegate
    public int winScore = 3;

    //set up the Singleton pattern
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            gameUI.onStartGame += ResetScoreboard;
        }
    }

    public void OnScoreZoneReached(int id)
    {
        if (id == 1)
            scorePlayer1++;

        if (id == 2)
            scorePlayer2++;

        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);
        CheckWin();
    }

    private void CheckWin()
    {
        int winnerId = scorePlayer1 == winScore ? 1 : scorePlayer2 == winScore ? 2 : 0;
        if (winnerId != 0)
        {
            gameUI.OnGameEnds(winnerId);
        }
        else
        {
            onReset?.Invoke();
        }
    }

    public void ResetScoreboard()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
    }

    public void LaunchBall()
    {
        gameUI.LaunchBall();
    }
}
