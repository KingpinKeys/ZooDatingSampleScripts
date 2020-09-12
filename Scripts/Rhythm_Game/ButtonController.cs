using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer imageComponent;
    public Sprite defaultImage;
    public Sprite onpressImage;

    public KeyCode inputKey;

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            imageComponent.sprite = onpressImage;
        }
        else if (Input.GetKeyUp(inputKey))
        {
            imageComponent.sprite = defaultImage;
        }
    }
}
