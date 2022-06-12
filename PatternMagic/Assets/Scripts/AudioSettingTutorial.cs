
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSettingTutorial : MonoBehaviour
{
    //private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    //private float backgroundFloat;
    private float soundEffectsFloat;
    //public AudioSource backgroundAudio;
    public AudioSource[] soundEffectsAudio;

    //private static AudioSettingTutorial instance = null;
    //public static AudioSettingTutorial Instance
    //{

    //    get { return instance; }
    //}

    void Awake()
    {
        //if (instance != null && instance != this)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}
        //else
        //{

        //    instance = this;
        //}
        //DontDestroyOnLoad(this.gameObject);
        soundEffectsFloat = 0.75f;
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        //backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
        soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

        
        //backgroundAudio.volume = backgroundFloat;

        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsFloat;
        }
    }

}
