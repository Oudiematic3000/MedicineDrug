using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxLives=5;
    public int currentLives;
    public int score=0;
    public static GameManager instance;
    public bool gamePaused=false;
    public float shiftTime = 300f, timerCurrent;
    public int patientsToWin=6;
    public TextMeshProUGUI scoreText, shiftTimerText;
    public Animator clipBoard;
    public AudioClip clipBoardSound;
    public GameObject[] hearts;
    bool shiftOver=false, dutiesDone=false;
    private void Awake()
    {
        if(!instance)instance = this;
    }
    void Start()
    {
        currentLives = maxLives;
        scoreText.text=score.ToString();
        timerCurrent = shiftTime;

    }

    public void SubtractLife()
    {
        Destroy(hearts[currentLives - 1]);
        currentLives--;
        if (currentLives <= 0) Lose();
    }

    public void Lose()
    {

    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score >= patientsToWin)
        {
         ValidateShift();
        }
    }
    public void PauseGame()
    {
        gamePaused = true;
    }
    public void UnpauseGame()
    {
        gamePaused=false;
    }

    public void ValidateShift()
    {
        if (shiftOver) return;
        shiftOver = true;
        if (score < patientsToWin)
        {
            Lose();
        }
        else
        {
            clipBoard.Play("ClipboardSlide");
            AudioManager.instance.PlaySFX(clipBoardSound);
        }

    }
    private void Update()
    {
        if (timerCurrent > 0)
        {
            timerCurrent -= Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timerCurrent);
            string formattedTime = string.Format("{0:00}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            shiftTimerText.text = formattedTime.ToString();
        }
        else
        {
            shiftTimerText.enabled = false;
            ValidateShift();
        }
    }
}
