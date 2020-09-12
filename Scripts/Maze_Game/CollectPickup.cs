using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPickup : MonoBehaviour
{
    Vector3 moveDirection;
    Vector3 transformDirection = new Vector3(-31.75f, 7.06f, -8.0f);

    public float speed = 3.0f;
    public int pickupValue;
    public GameObject timerIcon;

    public ActiveAnimation animationScript;

    bool pickedup;

    public AudioSource pickupSound;

    float animationRate = 1.0f;
    float nextAnimation;

    private void Start()
    {
        ScoringSystem.scoreValue = 0;
    }

    // If the pickup is collided with play sound effect and add score to current score
    void OnTriggerEnter(Collider other)
    {
        pickedup = true;
        pickupSound.Play();
        ScoringSystem.scoreValue += pickupValue;
    }

    private void Update()
    {
        // Rotate the pickup while it is alive, if picked up begin animation and then destroy the pickup
        float step = speed * Time.deltaTime;
        if (pickedup && transform.position != transformDirection)
        {
            transform.position = Vector3.MoveTowards(transform.position, transformDirection, step);
        }
        else if (pickedup)
        {
            timerIcon.SetActive(true);
            animationScript.Start();
            Destroy(gameObject);
        }
    }
}
