using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public int noOfLevels;
    public GameObject levelButton;
    public RectTransform ParentPanel;
    int levelReached;

    private void Awake()
    {
        LevelButtons();
    }

    void LevelButtons()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            levelReached = PlayerPrefs.GetInt("level"); //get the value from the key

        }
        else //the player is starting a new game
        {
            PlayerPrefs.SetInt("level", 1);
            levelReached = PlayerPrefs.GetInt("level"); //get the value from the key
        }

       
        for (int i = 0; i < noOfLevels; i++)
        {
            int x = new int();
            x = i + 1;

            GameObject lvlButton = Instantiate(levelButton);

            lvlButton.transform.SetParent(ParentPanel, false);
            Text buttonText = lvlButton.GetComponentInChildren<Text>();
            buttonText.text = (i + 1).ToString();

            lvlButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                LevelSelected(x);

            });
        }


    }

    void LevelSelected(int index) //collect integer
    {
        PlayerPrefs.SetInt("levelSelected", index);
        Debug.Log("Level Selected: " + index.ToString());
        Invoke(nameof(LoadGameplay),1f); //call game after 1 second
    }

    void LoadGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }
}

