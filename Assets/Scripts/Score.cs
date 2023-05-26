using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score;
    Text scoreText;
    int highScore = 0;

    public Text panelScore;
    public Text panelHighScore;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
        panelScore.text = score.ToString();

        PlayerPrefs.GetInt ("highScore", highScore);
        panelHighScore.text = score.ToString();
    }
    public void Scored()
    {
        score++;
        scoreText.text = score.ToString();
        panelScore.text = score.ToString();
        if(score>highScore)
        {
            highScore = score;
            panelHighScore.text = score.ToString();
            PlayerPrefs.SetInt ("highScore", highScore);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
