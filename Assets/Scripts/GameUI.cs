using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject menuObject;
    public TextMeshProUGUI winTitle;
    public GameObject countdown;
    private bool winnerDisplayed = false;

    private float timer = 0f;
    private readonly float waitTime = 15f;

    public Action onStartGame;

    private void Start()
    {
        countdown.SetActive(false);
    }

    private void Update()
    {
        if (winnerDisplayed)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                winTitle.text = "PongPong";
                GameManager.instance.ResetScoreboard();
                winnerDisplayed = false;
                timer = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && menuObject.activeSelf)
        {
            OnStartButtonClicked();
        }
    }

    public void UpdateScoreTexts(int scorePlayer1, int scorePlayer2)
    {
        scoreTextPlayer1.SetScore(scorePlayer1);
        scoreTextPlayer2.SetScore(scorePlayer2);
    }

    public void HighlightScore(int id)
    {
        if (id == 1)
            scoreTextPlayer1.Highlight();
        else
            scoreTextPlayer2.Highlight();
    }

    public void OnStartButtonClicked()
    {
        GameManager.instance.ResetScoreboard();
        menuObject.SetActive(false);
        winnerDisplayed = false;
        timer = 0f;
        countdown.SetActive(true);
    }

    public void LaunchBall()
    {
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winnerId) 
    {
        menuObject.SetActive(true);
        winTitle.text = $"Player {winnerId} wins!";
        winnerDisplayed = true;
    }
}
