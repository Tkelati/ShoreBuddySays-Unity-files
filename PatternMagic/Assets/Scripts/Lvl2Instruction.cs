using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Instruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Video.VideoPlayer videobox = GetComponent<UnityEngine.Video.VideoPlayer>();
        videobox.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Level2.mp4");
        videobox.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
