using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float highScore;
    public static int allCoins;
    public Text highScoreText;
    public Text allCoinsText;
    public GameObject resetPanel;
    // Start is called before the first frame update
    void Start()
    {
      
        //Diem cao nhat
        highScore = PlayerPrefs.GetFloat("endScore");
        highScoreText.text = "HighScore: " + ((int)highScore).ToString();

        //tong coins
        allCoins = PlayerPrefs.GetInt("allCoins");
        allCoinsText.text = allCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        allCoins = PlayerPrefs.GetInt("endCoins");
        PlayerPrefs.SetInt("allCoins",allCoins);
        highScoreText.text = "HighScore: " + ((int)highScore).ToString();
        allCoinsText.text = allCoins.ToString();
       
    }
    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Quit()
    {
        Application.Quit();    
    }
    public void ResetGameYes()
    {
        PlayerPrefs.DeleteAll();//xoa tat ca
        /* PlayerPrefs.DeleteKey("endCoins");//xoa moi tong vang
         PlayerPrefs.DeleteKey("endScore");// xoa moi diem cao nhat*/
        resetPanel.SetActive(false);
    }
    public void ResetGameNo()
    {
        resetPanel.SetActive(false);
    }
    public void ResetGame()
    {
        resetPanel.SetActive(true);

    }
}
