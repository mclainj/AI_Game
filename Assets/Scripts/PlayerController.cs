using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    public Camera sceneCam;
    public NavMeshAgent agent;
    [SerializeField] GameObject target;

    //Rigidbody rigidbody = new Rigidbody();

    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xControlSpeed = 40f;
    [Tooltip("In ms^-1")] [SerializeField] float zControlSpeed = 40f;
    float xThrow, zThrow;

    public Vector3 startingPos;
    private Vector3 lastPos;
    public int distanceRoll = 5;
    [SerializeField] float distRollMultiplier = 100;
    public float distanceTravelled = 0;

    public bool moveTurn = false;
    public bool gateTurn = false;
    public bool changedTurns = false;
    [SerializeField] GameLogic manager;
    [SerializeField] GameOverUI gameOverUI;

    // Update is called once per frame

    private void Start()
    {
        startingPos = transform.position;
        //rigidbody = GetComponent<Rigidbody>();
        //manager = GetComponent<GameLogic>();
        lastPos = transform.position;
    }
    void Update()
    {
        //PathfindToMouseClick();
        if (moveTurn)
        {
            ProcessInput();
        }
    }

    private void PathfindToMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = sceneCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void ProcessInput()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPos);

        if (distanceTravelled <= distanceRoll * distRollMultiplier)
        {
            ProcessTranslation();
            // todo fix collision issue with enemy that causes player drift
        } else
        { // prepare for next round
            moveTurn = false;
            lastPos = transform.position;
            ChangeTurns();
        }
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xControlSpeed * Time.deltaTime;
        float xPos = transform.position.x + xOffset;

        zThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float zOffset = zThrow * zControlSpeed * Time.deltaTime;
        float zPos = transform.position.z + zOffset;

        transform.position = new Vector3(xPos, transform.position.y, zPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("player triggered");
        if (other.tag == "Finish")
        {
            print("Victory! You win!");
            transform.GetChild(0).transform.gameObject.SetActive(true);
            gameOverUI.UpdateVictory();
        }
    }

    public void ChangeTurns()
    {
        if (!changedTurns)
        {
            manager.GetComponent<GameLogic>().AITurn();
            changedTurns = true;
        }
    }

}
