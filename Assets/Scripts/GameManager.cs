using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static Vector2 bottomLeft;
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool gameStarted; 

    private void Awake ()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint (new Vector2(0,0));
    }
    // Start is called before the first frame update
    public void restartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Start()
    {
        gameOver = false;
        gameStarted = false;
    }

    public void GameHasStarted ()
    {
        gameStarted = true;
    }

    public void GameOver ()
    {
        gameOver = true;
        gameOverPanel.SetActive (true);

    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
