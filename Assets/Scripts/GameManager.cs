using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //singleton

    public GameUI gameUI;
    public Ball ball;
    public Audio audio;
    public Spawner spawner;
    public int scorePlayer1, scorePlayer2;    
    public Action onReset;  //delegate
    public int winScore = 3;
    public int lastPlayed;  

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !gameUI.menuObject.activeSelf)
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : Time.timeScale == 1 ? 0 : Time.timeScale;
        }
    }

    public void OnScoreZoneReached(int id)
    {
        if (id == 1)
            scorePlayer1++;

        if (id == 2)
            scorePlayer2++;

        gameUI.UpdateScoreTexts(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);
        audio.PlayScoredSound();
        spawner.ResetSpawner();
        bool haveWinner = CheckWin();
        if (!haveWinner)
        {
            onReset?.Invoke();
        }
    }

    public void OnPlusOnePickedUp(PlusOne plusOne)
    {
        if(lastPlayed == 1)
        {
            scorePlayer1++;
        }

        if(lastPlayed == 2)
        {
            scorePlayer2++;
        }
        gameUI.UpdateScoreTexts(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(lastPlayed);
        audio.PlayScoredSound();
        plusOne.OnDestroy();
        CheckWin();
    }

    public void OnExtendPaddlePickedUp(ExtendPaddle extendPaddle)
    {
        if(lastPlayed == 1)
        {
            leftPaddle.Extend();
        }
        else if (lastPlayed == 2)
        {
            rightPaddle.Extend();
        }
        extendPaddle.OnDestroy();
    }

    private bool CheckWin()
    {
        int winnerId = scorePlayer1 == winScore ? 1 : scorePlayer2 == winScore ? 2 : 0;
        if (winnerId != 0)
        {
            gameUI.OnGameEnds(winnerId);
            ball.transform.position = ball.initialPosition;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetScoreboard()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.UpdateScoreTexts(scorePlayer1, scorePlayer2);
    }

    public void LaunchBall()
    {
        gameUI.LaunchBall();
    }
}
