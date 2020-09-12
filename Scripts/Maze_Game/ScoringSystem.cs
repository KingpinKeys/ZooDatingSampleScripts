using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public AudioSource tickSound;

    public float currentTime = 0.0f;
    public float startingTime = 30.0f;
    public float newTime = 0.0f;

    float interval = 1.0f;
    float trackedTime = 0.0f;

    public GameObject scoreText;
    public static int scoreValue;

    public bool gameOver;
    public bool hasStarted;

    void Start()
    {
        scoreText.GetComponent<Text>().text = "Time: 00:" + newTime.ToString("00");

        if (interval < 1.0f)
        {
            Debug.LogError("Interval must exceed 1.0!");
        }

        currentTime = startingTime;
        newTime = startingTime;
        gameOver = false;
        hasStarted = false;
    }

    void Update()
    {
        if (gameOver == false && hasStarted == true)
        {
            // Decrement timer every second and calculate new time based on current time and score
            currentTime -= 1 * Time.deltaTime;
            newTime = currentTime + scoreValue;

            // Timer hit zero, set to 0 to avoid negatives and call game over
            if (newTime <= 0.0f)
            {
                newTime = 0.0f;
                FindObjectOfType<MazeGameManager>().EndGame();
            }

            scoreText.GetComponent<Text>().text = "Time: 00:" + newTime.ToString("00");

            // Tracking functionaility for playing sound effect every second to match countdown
            trackedTime += Time.deltaTime;
            if (trackedTime >= interval)
            {
                tickSound.Play();
                trackedTime = 0.0f;
            }

        }
    }

}
