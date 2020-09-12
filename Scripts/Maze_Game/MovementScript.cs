using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{
    public LayerMask clickable;

    private NavMeshAgent myAgent;
    private Transform myTransform;
    public AnimationManager pandaAnim;

    public bool canMove;
    public bool cooldownStarted;

    public float moveCD = 3f;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myTransform = GetComponent<Transform>();
        canMove = true;
        cooldownStarted = false;
    }

    private void Update()
    {
        // Decremenet cooldown if the cooldown has been triggered
        if (cooldownStarted == true)
        {
            moveCD -= 1 * Time.deltaTime;
        }

        // Only allow movement if movement is allowed
        if (Input.GetMouseButton(0) && canMove == true)
        {
            cooldownStarted = true;
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            pandaAnim.SetAnimation("isWalking", true);

            if (Physics.Raycast(myRay, out hitInfo, 100, clickable))
            {
                myAgent.SetDestination(hitInfo.point);
            }

        }
        // If cooldown is off then allow movement
        if (moveCD <= 0.0f)
        {
            pandaAnim.SetAnimation("isWalking", false);
            myAgent.SetDestination(myTransform.position);
            cooldownStarted = false;
            moveCD = 3f;
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Finish")
        {
            // Find the game manager and end the game
            FindObjectOfType<MazeGameManager>().EndGame();
        }
    }
}
