using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Page8Manager : MonoBehaviour
{
    public static Page8Manager instance;
    public Page8ProgressBar _progressBar;
    public Page8FoxController foxController;
    public Page8SpawnShoe spawnShoe;
    public Page8SpawnStar spawnStar;
    public MSAudioManager audioManager;

    public Text scoreText;
    public Text resultScoreText;
    public GameObject resultPanel;

    private int score = 0;
    private float timeRemaining = 0f;
    private float timeLimit = 40f;
    
    private bool isPlaying = false;
    private bool inGame = false;
    private bool firstTimeAppear = true;

    public bool IsPlaying
    {
        get
        {
            return isPlaying;
        }
        set
        {
            isPlaying = value;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        ResetTimeLimit();
        ResetScore();
    }

    private void Update()
    {
        if(isPlaying)
        {
            timeRemaining -= Time.deltaTime;
            float progressPercentage = timeRemaining / timeLimit;
            _progressBar.UpdateProgress(progressPercentage);

            if(timeRemaining <= 0)
            {
                EndGame();
                resultScoreText.text = "Score: " + score.ToString();
                ClearItemsOnScene();
                resultPanel.SetActive(true);
            }
        }
    }

    void ClearItemsOnScene()
    {
        spawnShoe.ClearShoeOnScene();
        spawnStar.ClearStarOnScene();
    }

    public void Reset()
    {
        foxController.ResetPosition();
        ClearItemsOnScene();
        ResetScore();
        ResetTimeLimit();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseScore()
    {
        audioManager.PlaySFX(0);
        score += 10;
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddTime()
    {
        audioManager.PlaySFX(1);
        timeRemaining += 10f;
        if (timeRemaining > timeLimit)
        {
            timeRemaining = timeLimit;
        }
        //DisplayTime();
    }

    public void ReduceTime()
    {
        audioManager.PlaySFX(2);
        timeRemaining -= 10f;
        if(timeRemaining < 0)
        {
            timeRemaining = 0;
        }
        //DisplayTime();
    }

    public void ResetTimeLimit()
    {
        timeRemaining = timeLimit;
        float progressPercentage = timeRemaining / timeLimit;
        _progressBar.UpdateProgress(progressPercentage);
        //DisplayTime();
    }

    public void StartGame()
    {
        inGame = true;
        IsPlaying = true;
    }

    public void EndGame()
    {
        inGame = false;
        IsPlaying = false;
    }

    public void onLostCheckInGame()
    {
        if(inGame)
        {
            IsPlaying = false;
        }
        audioManager.PauseMusic();
    }

    public void onFoundCheckInGame()
    {
        if(firstTimeAppear)
        {
            audioManager.PlayMusic();
            firstTimeAppear = false;
        }
        if (inGame)
        {
            IsPlaying = true;
        }
        audioManager.UnPauseMusic();
    }

    //void DisplayTime()
    //{
    //    TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
    //    timeText.text = time.ToString(@"mm\:ss");
    //}


}
