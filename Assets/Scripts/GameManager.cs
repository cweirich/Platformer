using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int highScore = 0;
    private int level = 1;
    private HUDManager hudManager;
    private int currentTries;
    
    public int maxLevel = 2;
    public int maxTries = 2;

    public static GameManager instance;

    public int Score { get => score; set => score = value; }
    public int HighScore { get => highScore; set => highScore = value; }
    public int CurrentTries { get => currentTries; set => currentTries = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        currentTries = maxTries;

        hudManager = FindObjectOfType<HUDManager>();

        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;

        if (score > highScore)
            highScore = score;

        hudManager.ResetHUD();
    }

    public void ResetGame()
    {
        score = 0;
        level = 1;
        currentTries = maxTries;

        hudManager.ResetHUD();
        SceneManager.LoadScene("Level 1");
    }

    public void IncreaseLevel()
    {
        if (level < maxLevel)
            level++;
        else
            level = 1;

        hudManager.ResetHUD();
        SceneManager.LoadScene("Level " + level);
    }

    public void LoseTry()
    {
        if (currentTries > 0)
        {
            currentTries--;
            hudManager.ResetHUD();
        }

        else
            GameOver();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
