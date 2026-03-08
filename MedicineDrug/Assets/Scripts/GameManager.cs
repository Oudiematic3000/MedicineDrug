using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxLives=5;
    public int currentLives;
    public int score=0;
    public static GameManager instance;
    public bool gamePaused=false;

    public TextMeshProUGUI scoreText;
    public GameObject[] hearts;
    private void Awake()
    {
        if(!instance)instance = this;
    }
    void Start()
    {
        currentLives = maxLives;
        scoreText.text=score.ToString();
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
    }
    public void PauseGame()
    {
        gamePaused = true;
    }
    public void UnpauseGame()
    {
        gamePaused=false;
    }
}
