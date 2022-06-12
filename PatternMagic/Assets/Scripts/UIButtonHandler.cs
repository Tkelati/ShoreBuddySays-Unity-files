using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    

    public void loadMainMenu()

    {
        Destroy(FindObjectOfType<ContinuousBGMusic>());
        SceneManager.LoadScene("MainMenu");
        
    }
    
    public void loadLevel1()
    {
        
        SceneManager.LoadScene("GameScene");
        
    }

    public void loadLevelsMenu()
    {
      
        SceneManager.LoadScene("LevelMenu");
        
    }
    public void loadSoundMenu()
    {
        
        SceneManager.LoadScene("SoundMenu");
        
    }
    public void loadLevel2()
    {
        
        SceneManager.LoadScene("GameSceneLvl2");
        
    }
    public void loadLevel3()
    {

        SceneManager.LoadScene("GameSceneLvl3");

    }

    public void loadInstructLevel1()
    {
        SceneManager.LoadScene("Inst_Lvl1");
    }

    public void loadInstructLevel2()
    {
        SceneManager.LoadScene("Inst_Lvl2");
    }

    public void loadInstructLevel3()
    {
        SceneManager.LoadScene("Inst_Lvl3");
    }
    public void loadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

}
