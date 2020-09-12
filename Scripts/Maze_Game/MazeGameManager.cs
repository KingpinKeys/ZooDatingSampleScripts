using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeGameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    public GameObject resultsScreen, splashScreen;
    public GameObject scoreUI;
    public GameObject starOne, starTwo, starThree;

    public Text mazeCompleteText, timeLeftText;
    public float timeLeft;

    void Start()
    {
        // Disable player movement and score UI
        FindObjectOfType<MovementScript>().canMove = false;
        scoreUI.SetActive(false);
    }

    void Update()
    {
        // If input is registered, start game
        if (Input.anyKeyDown)
        {
            // Disable splash screen, enable movement and begin timer mechanic
            splashScreen.SetActive(false);
            scoreUI.SetActive(true);
            FindObjectOfType<MovementScript>().canMove = true;
            FindObjectOfType<ScoringSystem>().hasStarted = true;
        }
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            // Restart / show results
            if (!resultsScreen.activeInHierarchy)
            {
                // Stop audio sources
                FindObjectOfType<ScoringSystem>().gameOver = true;

                // Save the amount of time the player had left upon finish
                timeLeft = FindObjectOfType<ScoringSystem>().newTime;

                // Enable results screen and deactivate scoring UI
                resultsScreen.SetActive(true);
                scoreUI.SetActive(false);

                // Disable player movement
                FindObjectOfType<MovementScript>().canMove = false;

                // Set text game objects
                timeLeftText.text = timeLeft.ToString("00");

                if (timeLeft <= 0)
                {
                    mazeCompleteText.text = "Maze Failed!";
                }

                // Calculate star rating
                starOne.SetActive(false);
                starTwo.SetActive(false);
                starThree.SetActive(false);

                
                if (timeLeft > 5)
                {
                    starOne.SetActive(true);
                    if (timeLeft > 10)
                    {
                        starTwo.SetActive(true);
                        if (timeLeft > 15)
                        {
                            starThree.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
