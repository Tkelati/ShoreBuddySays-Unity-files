using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScene : MonoBehaviour
{
    private float waitUntilFade;

    // Start is called before the first frame update
    void Start()
    {
        waitUntilFade = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitUntilFade >= 0)
        {
            waitUntilFade -= Time.deltaTime;
        }
        else
        {
            Canvas temp = FindObjectOfType<Canvas>();
            Text[] txtchildren = temp.GetComponentsInChildren<Text>();
            Image[] imgchildren = temp.GetComponentsInChildren<Image>();
            foreach (var child in txtchildren){
                child.GetComponent<Text>().enabled=false;
            }
            foreach (var child in imgchildren)
            {
                if (child.name == "beachview")
                {
                    continue;
                }
                else
                {
                    child.GetComponent<Image>().enabled = false;
                }
            }
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out, "MainMenu"));
        }
    }
}
