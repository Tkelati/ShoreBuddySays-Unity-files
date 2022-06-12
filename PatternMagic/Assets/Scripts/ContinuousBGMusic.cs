using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousBGMusic : MonoBehaviour
{
    public static AudioSource music;
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private static readonly string FirstPlay = "FirstPlay";
    private int firstPlayInt = 0;
    private static float backgroundFloat;


    private static ContinuousBGMusic instance = null;
    public static ContinuousBGMusic Instance
    {

        get { return instance; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            music = GetComponent<AudioSource>();
            firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
            Debug.Log("Value of FirstPlayInt is" + firstPlayInt);
            if (firstPlayInt == 0)
            {
                backgroundFloat = 0.25f;

                PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
                Debug.Log("if firstplayint is 0, Setting backgroundFloat to" + backgroundFloat);
                PlayerPrefs.SetFloat(SoundEffectsPref, 0.75f);
                PlayerPrefs.SetInt(FirstPlay, -1);

            }
            else
            {
                Debug.Log("if firstplayint is not 0, Setting backgroundFloat to" + backgroundFloat);
                createPref();
                backgroundFloat =PlayerPrefs.GetFloat(BackgroundPref);          
            }
            music.volume = backgroundFloat;
            music.Play();
            instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
    
    void createPref()
    {
        PlayerPrefs.SetFloat(BackgroundPref, 0.1f);
        PlayerPrefs.SetFloat(SoundEffectsPref, 0.75f);
        PlayerPrefs.Save();
    }

    //void Awake()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //    else
    //    {
    //        //backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
    //        //music.volume = backgroundFloat;
    //        instance = this;
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}
}