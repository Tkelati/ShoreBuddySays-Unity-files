using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameCode : MonoBehaviour
{
    public SpriteRenderer[] colors;
    public AudioSource[] buttonSounds;

    private int colorSelect;

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
        
    }

    public bool isGameActive()
    {
        return gameActive;
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

    public void StartGame()//game code used the run the game and the random colors picked
    {
        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;
        trackRounds = 1;
        

        colorSelect = Random.Range(0, colors.Length);
        activeSequence.Add(colorSelect);

        

        
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
                //code to add the the sequence if the play presses the right buttons
                if(inputInSequence >= activeSequence.Count)
                {
                    positionInSequence = 0;
                    inputInSequence = 0;

                    colorSelect = Random.Range(0, colors.Length);

                    activeSequence.Add(colorSelect);

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
                feedback.text = "Sorry, play again!";
                incorrect.Play();
                trackRounds = 1;

                gameActive = false;
            }
        }
    }
}