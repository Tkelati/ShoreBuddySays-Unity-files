using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameCodeLvl3 : MonoBehaviour
{
    public SpriteRenderer[] colors;
    public AudioSource[] buttonSounds;

    public Sprite[] nonrecyclables;
    public Sprite[] recyclables;
    public SpriteRenderer[] placeholders;

    private int colorSelect;
    private int disabledColor1;
    private int disabledColor2;

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

    public bool isGameActive()
    {
        return gameActive;
    }

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

    public void StartGame()//game code used the run the game and the random colors picked
    {
        Debug.Log("Started game");
        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;
        trackRounds = 1;

        //choosing the color of buttons that will have nonrecyclables this game      
        disabledColor1 = Random.Range(0, colors.Length);
        disabledColor2 = Random.Range(0, colors.Length);
        while (disabledColor2 == disabledColor1)
        {
            //need to make sure disabled colors are different
            disabledColor2 = Random.Range(0, colors.Length);

        }

        colorSelect = Random.Range(0, colors.Length);
        Debug.Log("Chose selected color, = " + colorSelect.ToString());

        while (colorSelect == disabledColor1 || colorSelect == disabledColor2)
        {
            //first color selection cannot be the same as one of the disabled colors or the game cannot start
            colorSelect = Random.Range(0, colors.Length);
            Debug.Log("While loop... colorSelect = " + colorSelect.ToString());
        }
        activeSequence.Add(colorSelect);

        //choosing 1 of x available nonrecyclables to use for sprite on disabledColor buttons
        var nonrecyclableSprite1 = nonrecyclables[0];
        var nonrecyclableSprite2 = nonrecyclables[1];
        //var nonrecyclableSprite1 = nonrecyclables[Random.Range(0, nonrecyclables.Length)];
        //var nonrecyclableSprite2 = nonrecyclables[Random.Range(0, nonrecyclables.Length)];
        //while (nonrecyclableSprite1 == nonrecyclableSprite2)
        //{
        //    nonrecyclableSprite2 = nonrecyclables[Random.Range(0, nonrecyclables.Length)];
        //}

        //using list because we can remove items as we choose them
        List<Sprite> recyclablesToChoose = new List<Sprite>(); 
        for (int i = 0; i < recyclables.Length; i++)
        { //populate list from original array
            recyclablesToChoose.Add(recyclables[i]);
        }
        Debug.Log("Converted array to list");

        var recyclablesChosen = new Sprite[3];
        //choosing 3 of 5 availalbe recyclables to use for sprites
        for (int i = 0; i < 3; i++)
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
            
            if (i == disabledColor1)
            {
                placeholders[i].sprite = nonrecyclableSprite1;
                placeholders[i].color = new Color(255, 255, 255, 1.0f);
            }
            else if (i == disabledColor2)
            {
                placeholders[i].sprite = nonrecyclableSprite2;
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

                while (inputInSequence < activeSequence.Count && (activeSequence[inputInSequence] == disabledColor1 || activeSequence[inputInSequence] == disabledColor2))
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