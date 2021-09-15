using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentCharIndex = 0;
    public GameObject[] characterModels;
    public CharacterInfor[] characters;
    public Button buyCharacter;
    public Text textBuy;

    // Start is called before the first frame update
    void Start()
    {
        foreach (CharacterInfor charac in characters)
        {
            if (charac.price == 0)
                charac.isUnlocked = true;
            else
                charac.isUnlocked = PlayerPrefs.GetInt(charac.name, 0) == 0 ? false : true;//goi bien nho charac.name, neu ko ton tai gia tri thi se mac dinh co gia tri la 0
        }
        currentCharIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach(GameObject charMod in characterModels)
        {
            charMod.SetActive(false);
        }
        characterModels[currentCharIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void Next()
    {
        characterModels[currentCharIndex].SetActive(false);
        currentCharIndex++;
        if (currentCharIndex == characterModels.Length)
        {
            currentCharIndex = 0;
        }
        characterModels[currentCharIndex].SetActive(true);
        CharacterInfor c = characters[currentCharIndex];
        if (!c.isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCharacter", currentCharIndex);
    }
    public void Previous()
    {
        characterModels[currentCharIndex].SetActive(false);
        currentCharIndex--;
        if (currentCharIndex == -1)
        {
            currentCharIndex = characterModels.Length - 1;
        }
        characterModels[currentCharIndex].SetActive(true);
        CharacterInfor c = characters[currentCharIndex];
        if (!c.isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCharacter", currentCharIndex);

    }
    void UpdateUI()//lam viec voi giao dien
    {
        CharacterInfor c = characters[currentCharIndex];
        if (c.isUnlocked)//neu da unlock thi khong hien thi nut Buy
            buyCharacter.gameObject.SetActive(false);
        else
        {
            buyCharacter.gameObject.SetActive(true);
            textBuy.text="Buy-"+c.price.ToString();//hien thi gia tien tren nut Buy
            if (c.price <= MainMenu.allCoins)
                buyCharacter.interactable = true;//co the tuong tac voi nut
            else
                buyCharacter.interactable = false;//khong the tuong tac
        }
    }
    public void UnlockedCharacter()//mo khoa nhan vat
    {
        CharacterInfor c = characters[currentCharIndex];
        PlayerPrefs.SetInt(c.name, 1);//gan gia tri cho c.name la 1
        PlayerPrefs.SetInt("SelectedCharacter", currentCharIndex);
        c.isUnlocked = true;
        PlayerPrefs.SetInt("endCoins", PlayerPrefs.GetInt("endCoins") - c.price);
       
    }
}
