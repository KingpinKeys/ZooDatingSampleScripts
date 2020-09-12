using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollController : MonoBehaviour
{

    public float beatTempo;
    public bool hasStarted;
    
    public float timer;
    private float startTime;
    public float exitTime;
    public SObjStudbook studbook;

    // Start is called before the first frame update
    void Start()
    {
        // 120 bpm / 60secs = 2 beats per second
        beatTempo = beatTempo / 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
                startTime = Time.time;

            }
        }
        else
        {
            transform.position -= new Vector3(0.0f, beatTempo * Time.deltaTime, 0.0f);
        }

        ///
        if (hasStarted)
        {
            timer = Time.time - startTime;
        }

    }
}
