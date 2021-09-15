using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{
    public int currentCharIndex = 0;
    public GameObject[] characters;
    // Start is called before the first frame update
    void Start()
    {
        currentCharIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }
        characters[currentCharIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
