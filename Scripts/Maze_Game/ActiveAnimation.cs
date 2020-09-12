using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    public void Start()
    {
        Debug.Log("Pickup realised!");
        StartCoroutine(handleRotation());
    }

    // Show animation UI for 2 seconds and then disable again
    IEnumerator handleRotation()
    {
        yield return new WaitForSeconds(2);

        UI.SetActive(false);
    }

}
