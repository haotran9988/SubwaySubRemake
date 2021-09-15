using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    //coin and score
    public Text scoreText;
    public Text coinText;
    public static float score;
    public static int numberOfCoins;
    private int level = 1;
    private float scoreToNextLevel = 100;

    //game over
    public Text endGameScore;
    public Text endGameCoin;
    public static bool isGameOver;
    public GameObject gameOverPanel;

    //pause game
    public GameObject gamePausePanel;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        numberOfCoins = 0;
        isGameOver = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        score += 3 * Time.deltaTime * level;
        scoreText.text = ((int)score).ToString();
        coinText.text = numberOfCoins.ToString();
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
            endGameScore.text = ((int)score).ToString();
            endGameCoin.text = numberOfCoins.ToString();
            PlayerPrefs.SetInt("endCoins", numberOfCoins+MainMenu.allCoins);
            if (PlayerPrefs.GetFloat("endScore") < score)
            {
                PlayerPrefs.SetFloat("endScore", score);
            }
            
        }
        if (score > scoreToNextLevel)
        {
            level++;
            scoreToNextLevel *= 3;
            PlayerController.speed += 1;
        }
       
    }
    public void GamePause()
    {
        gamePausePanel.SetActive(true);
        FindObjectOfType<AudioManager>().PauseSound("MainTheme");
        Time.timeScale = 0;

    }
    public void Resume()
    {
        gamePausePanel.SetActive(false);
        FindObjectOfType<AudioManager>().PlaySound("MainTheme");
        Time.timeScale = 1;
    }
    public void Replay()
    {
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1;
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
