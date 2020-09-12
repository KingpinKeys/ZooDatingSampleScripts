using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public SObjStudbook studbook;
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("maze_game");
    }

    public void SwitchScenes(string s)
    {
        //switch scene based on name
        SceneManager.LoadScene(s, LoadSceneMode.Single);

        studbook.currentEntry.unlocked = true;
        studbook.currentEntry.DirtyThis();
    }

}
