using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    // Update is called once per frame
    public float rotationX, rotationY, rotationZ;

    void Update()
    {
        transform.Rotate(new Vector3(rotationX, rotationY, rotationZ));
    }
}
