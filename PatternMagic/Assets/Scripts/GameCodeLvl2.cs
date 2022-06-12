using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameCodeLvl2 : MonoBehaviour
{
    public SpriteRenderer[] colors;
    public AudioSource[] buttonSounds;

    public Sprite[] nonrecyclables;
    public Sprite[] recyclables;
    public SpriteRenderer[] placeholders;

    private int colorSelect;
    private int disabledColor;

    public float stayLit;
    private float stayLitCounter;

    public float waitToStart;
    private float waitToStartCounter;
    private bool startGameNow = false;

    public float waitBetweenLights; 
    public float waitBetweenRounds;
    private float waitBetweenCounter;

    private bool shouldBeLit;
    private bool shouldBeDark;

    public List<int> activeSequence;
    private int positionInSequence;

    private bool gameActive;
    private int inputInSequence;

    private int trackRounds;
    public Text roundNumber;
    public Text feedback;

    public AudioSource correct;
    public AudioSource incorrect;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loaded game");
    }

    // Update is called once per frame
    void Update()
    {
        roundNumber.text = trackRounds.ToString();

        //wait x seconds to start lighting buttons at beginning of game to let user look at buttons first
        if (startGameNow)
        {
            if (waitToStartCounter > 0)
            {
                waitToStartCounter -= Time.deltaTime;
            }
            else
            {
                colors[activeSequence[positionInSequence]].color = new Color(colors[activeSequence[positionInSequence]].color.r, colors[activeSequence[positionInSequence]].color.g, colors[activeSequence[positionInSequence]].color.b, 1f);
                buttonSounds[activeSequence[positionInSequence]].Play();

                stayLitCounter = stayLit;
                shouldBeLit = true;
                startGameNow = false;
            }
        }

        //code to tell if the button should be lit
        if (shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;

            if (stayLitCounter < 0)
            {
                colors[activeSequence[positionInSequence]].color = new Color(colors[activeSequence[positionInSequence]].color.r, colors[activeSequence[positionInSequence]].color.g, colors[activeSequence[positionInSequence]].color.b, 0.5f);
                buttonSounds[activeSequence[positionInSequence]].Stop();

                shouldBeLit = false;

                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLights;

                positionInSequence++;
            }
        }
        if (shouldBeDark)
        {
            waitBetweenCounter -= Time.deltaTime;

            if(positionInSequence >= activeSequence.Count)
            {
                shouldBeDark = false;
                gameActive = true;
                feedback.text = "";
            }
            else
            {
                if(waitBetweenCounter < 0)// time between button light ups
                {

                    colors[activeSequence[positionInSequence]].color = new Color(colors[activeSequence[positionInSequence]].color.r, colors[activeSequence[positionInSequence]].color.g, colors[activeSequence[positionInSequence]].color.b, 1f);
                    buttonSounds[activeSequence[positionInSequence]].Play();

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }

    public bool isGameActive()
    {
        return gameActive;
    }

    public void StartGame()//game code used the run the game and the random colors picked
    {
        Debug.Log("Started game");
        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;
        trackRounds = 1;

        //choosing the color of button that will have nonrecyclable this game      
        disabledColor = Random.Range(0, colors.Length);
        Debug.Log("Chose disabled color, = " + disabledColor.ToString());
        colorSelect = Random.Range(0, colors.Length);
        Debug.Log("Chose selected color, = " + colorSelect.ToString());

        while (colorSelect == disabledColor)
        {
            //first color selection cannot be the disabled color or the game cannot start
            colorSelect = Random.Range(0, colors.Length);
            Debug.Log("While loop... colorSelect = " + colorSelect.ToString());
        }
        activeSequence.Add(colorSelect);
        
        //choosing 1 of x available nonrecyclables to use for sprite on disabledColor button
        var nonrecyclableSprite = nonrecyclables[Random.Range(0, nonrecyclables.Length)];

        //using list because we can remove items as we choose them
        List<Sprite> recyclablesToChoose = new List<Sprite>(); 
        for (int i = 0; i < recyclables.Length; i++)
        { //populate list from original array
            recyclablesToChoose.Add(recyclables[i]);
        }
        Debug.Log("Converted array to list");

        var recyclablesChosen = new Sprite[4];
        //choosing 4 of 5 availalbe recyclables to use for sprites
        for (int i = 0; i < 4; i++)
        {
            var chosenRecyclableIndex = Random.Range(0, recyclablesToChoose.Count);
            Debug.Log("i = "+i+", Count = " + recyclablesToChoose.Count + ", chosen index = " + chosenRecyclableIndex);
            recyclablesChosen[i] = recyclablesToChoose[chosenRecyclableIndex];
            recyclablesToChoose.RemoveAt(chosenRecyclableIndex);
        }
        Debug.Log("Chose array of recyclables");

        //wire up sprites to buttons here
        int recyclableToRender = 0;
        for (int i = 0; i < placeholders.Length; i++)
        {
            
            if (i == disabledColor)
            {
                placeholders[i].sprite = nonrecyclableSprite;
                placeholders[i].color = new Color(255, 255, 255, 1.0f);
            }
            else
            {
                placeholders[i].sprite = recyclablesChosen[recyclableToRender];
                placeholders[i].color = new Color(255, 255, 255, 1.0f);
                recyclableToRender++;
            }
        }

        waitToStartCounter = waitToStart;
        startGameNow = true;

        
        
    }

    public void ColorPressed(int whichButton)// code to tells in the button pressed in correct or not 
    {
        if (gameActive)
        {

            if (activeSequence[inputInSequence] == whichButton)
            {
                Debug.Log("Correct");

                inputInSequence++;

                while (inputInSequence < activeSequence.Count && activeSequence[inputInSequence] == disabledColor)
                {
                    //when checking a button in the active sequence, skip over any buttons that are the disabled colour
                    //need to increment counter again to check input against next button
                    inputInSequence++;
                }
                //this means if the person does press the disabled colour, the sequence will be considered wrong because it will check against the next colour

                //code to add the the sequence if the play presses the right buttons
                if(inputInSequence >= activeSequence.Count)
                {
                    positionInSequence = 0;
                    inputInSequence = 0;

                    colorSelect = Random.Range(0, colors.Length);

                    activeSequence.Add(colorSelect);

                    //FOLLOWING CODE HERE ORIGINALLY, BUT IS HANDLED IN UPDATE FUNCTION ANYWAY
                    //colors[activeSequence[positionInSequence]].color = new Color(colors[activeSequence[positionInSequence]].color.r, colors[activeSequence[positionInSequence]].color.g, colors[activeSequence[positionInSequence]].color.b, 1f);
                    //buttonSounds[activeSequence[positionInSequence]].Play();
                    //stayLitCounter = stayLit;
                    //shouldBeLit = true;

                    shouldBeLit = false;
                    shouldBeDark = true;
                    waitBetweenCounter = waitBetweenRounds;

                    gameActive = false;
                    correct.Play();
                    feedback.text = "You got it!";
                    trackRounds++;
                }
            }
            else//code to end game if the wrong sequence is inputed
            {
                Debug.Log("Wrong");
                feedback.text = "Sorry, play again";
                incorrect.Play();
                trackRounds = 1;

                gameActive = false;
            }
        }
    }
}