using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public void Start()
    {
        scoreText.text = GameManager.instance.Score.ToString();
        highScoreText.text = GameManager.instance.HighScore.ToString();               
    }

    public void PlayAgain()
    {
        GameManager.instance.ResetGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
