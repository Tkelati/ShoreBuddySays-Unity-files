using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressLvl2 : MonoBehaviour
{

    private SpriteRenderer theSprite;
    public SpriteRenderer[] placeholders;
    private SpriteRenderer thePlaceholder;

    public int thisButtonNumber;

    private GameCodeLvl2 theGC;

    private AudioSource theSound;

    private float speed = 300.0f;
    private Vector2 target;
    private Vector2 position;
    private Vector2 savePosition;
    bool moveShoreBuddy;

    // Start is called before the first frame update
    void Start()
    {
        theSprite = GetComponent<SpriteRenderer>();
        theGC = FindObjectOfType<GameCodeLvl2>();
        theSound = GetComponent<AudioSource>();
        GameObject centerSpot = GameObject.Find("Logo");
        target = centerSpot.transform.position;
        thePlaceholder = placeholders[thisButtonNumber];
        position = thePlaceholder.transform.position;
        savePosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveShoreBuddy)
        {
            float step = speed * Time.deltaTime;
            // move sprite towards the target location
            thePlaceholder.transform.position = Vector2.MoveTowards(thePlaceholder.transform.position, target, step);
            position = thePlaceholder.transform.position;
            if (position == target)
            {
                
                moveShoreBuddy = false;
                thePlaceholder.transform.position = savePosition;
            }
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
            moveShoreBuddy = true;
        }
        
                

        theSound.Stop();
        

    }


}
