using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{

    private SpriteRenderer theSprite;
    public SpriteRenderer[] placeholders;
    private SpriteRenderer thePlaceholder;

    public int thisButtonNumber;

    private GameCode theGC;

    private AudioSource theSound;

    bool moveShoreBuddy;
    private int angleCount;
    private int angleToRotate = 15;
    private float speed = 400f;

    bool rotForward = true;
    bool rotBack = false;

    bool forwardDone = false;
    bool backDone = false;

    // Start is called before the first frame update
    void Start()
    {
        theSprite = GetComponent<SpriteRenderer>();
        theGC = FindObjectOfType<GameCode>();
        theSound = GetComponent<AudioSource>();

        thePlaceholder = placeholders[thisButtonNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (moveShoreBuddy)
        {

            float step = speed * Time.deltaTime;
            if (rotForward)
            {
                if (angleCount > 0)
                {
                    //thePlaceholder.transform.Rotate(Vector3.forward, step, Space.Self);
                    thePlaceholder.transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
                    //Debug.Log("Rotated forward");
                    angleCount--;
                    forwardDone = true;
                }
                else
                {
                    forwardDone = true;
                    rotBack = true;
                    rotForward = false;
                }
                
                
            }
            else if(rotBack)
            {
                if (angleCount < angleToRotate)
                {
                    //thePlaceholder.transform.Rotate(Vector3.back, step, Space.Self);
                    thePlaceholder.transform.Rotate(0.0f, 0.0f, -1.0f, Space.Self);
                    //Debug.Log("Rotated back");
                    angleCount++;
                }
                else
                {
                    rotBack = false;
                    rotForward = true;
                    backDone = true;
                }
                
            }
            
            if (forwardDone && backDone)
            {
                moveShoreBuddy = false;
                //Debug.Log("moveShoreBuddy = " + moveShoreBuddy);
                forwardDone = false;
                backDone = false;
            }
            //Debug.Log("moveShoreBuddy = " + moveShoreBuddy);

        }

    }

    void OnMouseDown()
    {
        if (theGC.isGameActive())
        {
            theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 1f);
            theSound.Play();
        }
    }
    void OnMouseUp()
    {
        if (theGC.isGameActive())
        {
            theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 0.5f);
            theGC.ColorPressed(thisButtonNumber);
            angleCount = angleToRotate;
            moveShoreBuddy = true;
        }
        
        theSound.Stop();

    }
    

}
