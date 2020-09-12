using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Variables for controlling/play music
    public AudioSource musicSource;
    public bool musicPlaying;

    // Instance of scroll controller so we can communicate to start music and scrolling at same time
    public ScrollController scrollControl;

    // Instance of game manager so that note controller can access functions for hit or miss
    public static GameManager instance;

    // Variables for tracking Score and Combo
    public int currentScore;
    public int scorePerHit = 100;
    public int scorePerBearificHit = 150;
    public int scorePerPandtasticHit = 125;

    public int currentCombo;
    public int comboTracker;
    public int[] comboThreshold;

    public Text scoreText;
    public Text comboText;

    public float totalNotes;
    public float goodHits;
    public float pandtasticHits;
    public float bearificHits;
    public float missedHits;

    public SObjStudbook studbook;

    public GameObject resultsScreen, starOne, starTwo, starThree, splashScreen;
    public Text percentHitText, goodText, pandtasticText, bearificText, missedText, rankText, finalScoreText;

    public AnimationManager leftPanda;
    public AnimationManager rightPanda;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.gameObject.SetActive(false);
        comboText.gameObject.SetActive(false);
        // Make instance a copy of game manager
        instance = this;
        
        // Init score text to 0
        scoreText.text = "Score: 0";
        comboText.text = "Combo: x1";
        currentCombo = 1;

        // Finds the total notes in the scene
        totalNotes = FindObjectsOfType<NoteController>().Length;
    }

    // Update is called once per frame
    void Update()
    {
     
        if(!musicPlaying)
        {
            if(Input.anyKeyDown)
            {
                // Game has started so display combo/score and remove splash screen
                scoreText.gameObject.SetActive(true);
                comboText.gameObject.SetActive(true);
                splashScreen.SetActive(false);

                musicPlaying = true;
                // The notes will start scrolling after input
                scrollControl.hasStarted = true;

                musicSource.Play();
            }
        }
        else
        {
            if(FindObjectsOfType<NoteController>().Length == 0 && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                starOne.SetActive(false);
                starTwo.SetActive(false);
                starThree.SetActive(false);

                goodText.text = goodHits.ToString();
                bearificText.text = bearificHits.ToString();
                pandtasticText.text = pandtasticHits.ToString();
                bearificText.text = bearificHits.ToString();
                missedText.text = missedHits.ToString();

                float totalHit = goodHits + bearificHits + pandtasticHits;
                float percentHit = (totalHit / totalNotes) * 100.0f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankValue = "F";

                if (percentHit > 40)
                {
                    rankValue = "D";
                    if (percentHit > 55)
                    {
                        rankValue = "C";
                        starOne.SetActive(true);
                        if(percentHit > 70)
                        {
                            rankValue = "B";
                            starTwo.SetActive(true);
                            studbook.currentEntry.unlocked = true;
                            if(percentHit > 85)
                            {
                                rankValue = "A";
                                if(percentHit > 95)
                                {
                                    rankValue = "S";
                                    starThree.SetActive(true);
                                }
                            }
                        }
                    }
                }

                rankText.text = rankValue;
                finalScoreText.text = currentScore.ToString();
            }
        }

    }

    public void NoteHit()
    {

        // Make sure not to run when all thresholds have been achieved
        if (currentCombo - 1 < comboThreshold.Length)
        {
            comboTracker++;

            // Check if tracker has passed threshold value
            if (comboThreshold[currentCombo - 1] <= comboTracker)
            {
                comboTracker = 0;
                currentCombo++;
            }
        }

        comboText.text = "Combo: x" + currentCombo;

        // Increase score everytime a note is hit
        scoreText.text = "Score: " + currentScore;
    }

    public void NoteMissed()
    {
        Debug.Log("Note Missed!");

        // Note missed so reset combo
        currentCombo = 1;
        comboTracker = 0;
        comboText.text = "Combo: x" + currentCombo;
    }

    public void GoodHit()
    {
        currentScore += scorePerHit * currentCombo;
        NoteHit();

        goodHits++;
    }
    public void PandtasticHit()
    {
        currentScore += scorePerPandtasticHit * currentCombo;
        NoteHit();

        pandtasticHits++;
    }

    public void BearificHit()
    {
        currentScore += scorePerBearificHit * currentCombo;
        NoteHit();

        leftPanda.TriggerAnimation("isSuccess");
        rightPanda.TriggerAnimation("isSuccess");

        bearificHits++;
    }

    public void MissHit()
    {
        
        NoteMissed();

        leftPanda.TriggerAnimation("isFail");
        rightPanda.TriggerAnimation("isFail");

        missedHits++;
    }
}
