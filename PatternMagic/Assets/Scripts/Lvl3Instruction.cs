using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Instruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Video.VideoPlayer videobox = GetComponent<UnityEngine.Video.VideoPlayer>();
        videobox.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Level3.mp4");
        videobox.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
