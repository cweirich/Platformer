using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private Text scoreLabel;
    private Text triesLabel;

    // Start is called before the first frame update
    void Start()
    {
        ResetHUD();
    }

    public void ResetHUD()
    {
        if (scoreLabel == null || triesLabel == null)
        {
            Text[] labels;            
            labels = FindObjectsOfType<Text>();
            scoreLabel = labels[1];
            triesLabel = labels[0];
        }

        scoreLabel.text = "Score: " + GameManager.instance.Score;
        triesLabel.text = "Tries: " + GameManager.instance.CurrentTries;
    }
}
