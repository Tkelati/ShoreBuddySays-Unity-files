using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public Text levelName;

    private void Start()
    {
        int index = PlayerPrefs.GetInt("levelSelected"); //retrieve value of the level selected
        levelName.text = "Level " + index.ToString();
        
    }
}
