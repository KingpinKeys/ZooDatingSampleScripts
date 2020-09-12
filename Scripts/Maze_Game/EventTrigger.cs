using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<MazeGameManager>().EndGame();
    }

}
