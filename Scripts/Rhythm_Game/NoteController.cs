using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{

    public bool canBePressed;
    public KeyCode keyToPress;

    public GameObject goodEffect, pandtasticEffect, bearificEffect, missEffect, noteParticle;

    private Vector3 sizeOffset = new Vector3(0f, 1.5f, 0.0f);
    private Vector3 missOffset = new Vector3(0f, 2f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);

                // Mathf absolute turns negative value to positive value
                if(Mathf.Abs(transform.position.y) > 1.25)
                {
                    Debug.Log("Good Hit");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position + sizeOffset, goodEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 1.05f)
                {
                    Debug.Log("Pandtastic Hit");
                    GameManager.instance.PandtasticHit();
                    Instantiate(pandtasticEffect, transform.position + sizeOffset, pandtasticEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Bearific Hit");
                    GameManager.instance.BearificHit();
                    Instantiate(bearificEffect, transform.position + sizeOffset, bearificEffect.transform.rotation);
                }



            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            // Note has exited bounds so no longer can be pressed
            canBePressed = false;

            if (gameObject.activeInHierarchy)
            {
                // Note missed!
                GameManager.instance.NoteMissed();
                Destroy(gameObject);

                Debug.Log("Miss Hit");
                GameManager.instance.MissHit();
                Instantiate(missEffect, transform.position + missOffset, missEffect.transform.rotation);
            }
        }
    }

}
