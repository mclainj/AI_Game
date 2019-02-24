using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;

public class Minotaur : MonoBehaviour
{
    public int distanceRoll = 5;
    [SerializeField] int distanceMultiplier = 2;
    public NavMeshAgent agentMinotaur;
    //public NavMeshAgent agentPlayer;
    PlayerController player;
    AI AI;
    private int gateMask;

    public GameObject finish;
    public GameObject pastMarker;
    public bool changedTurns = false;

    private Vector3 startingPos;
    //private Vector3 playerMoveLastPos;
    private Transform lastPos;

    //GameLogic manager;
    [SerializeField] GameLogic manager;

    [SerializeField] GameObject gateControl;

    public bool playerMinotaur = false;
    public bool AIMinotaur = false;
    [Header("Movement")]
    [Tooltip("In ms^-1")] [SerializeField] float xControlSpeed = 40f;
    [Tooltip("In ms^-1")] [SerializeField] float zControlSpeed = 40f;
    float xThrow, zThrow;

    public float distanceTravelled = 0f;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        AI = FindObjectOfType<AI>();
        agentMinotaur.isStopped = true;
        startingPos = transform.parent.transform.position;
        //playerMoveLastPos = transform.parent.transform.position;
        //lastPos = pastMarker.transform;
        lastPos = transform.parent.transform;
        gateMask = 1 << NavMesh.GetAreaFromName("Gate");
    }

    private void Update()
    {
        if (playerMinotaur)
        {
            ProcessInput();
        }
        else if (AIMinotaur)
        {
            Travel();
        }
        
    }
    private void ProcessInput()
    {
        print("parent pos: " + transform.parent.transform.position + " . Last pos : " + lastPos.position);
        distanceTravelled += Vector3.Distance(transform.parent.transform.position, lastPos.position);
        print("Entered ProcessInput. distanceTravelled: " + distanceTravelled + " <= " + distanceRoll * distanceMultiplier);
        if (distanceTravelled <= distanceRoll * distanceMultiplier)
        {
            ProcessTranslation();
            // todo fix collision issue with enemy that causes player drift
        }
        else
        { // prepare for next round
            playerMinotaur = false;
            //playerMoveLastPos = transform.parent.transform.position;
            lastPos = transform.parent.transform;
            //player.ChangeTurns();
        }
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xControlSpeed * Time.deltaTime;
        float xPos = transform.parent.transform.position.x + xOffset;

        zThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float zOffset = zThrow * zControlSpeed * Time.deltaTime;
        float zPos = transform.parent.transform.position.z + zOffset;

        transform.position = new Vector3(xPos, transform.parent.transform.position.y, zPos);
    }

    private void Travel()
    {
        if (!agentMinotaur.hasPath)
        {
            agentMinotaur.SetDestination(player.transform.position);
        }
        else
        {
            //print(getPathDistance(agentMinotaur, lastPos) + " / " + distanceRoll);
            if (getPathDistance(agentMinotaur, lastPos) >= distanceRoll * distanceMultiplier)
            {
                //print("STOP");
                agentMinotaur.isStopped = true;
                pastMarker.transform.position = agentMinotaur.transform.position;
                lastPos = pastMarker.transform;
                if (!changedTurns)
                {
                    manager.GetComponent<GameLogic>().PlayerTurn();
                    changedTurns = true;
                }
            }
        }
    }

    private float getPathDistance(NavMeshAgent agent, Transform target)
    {
        float distance = 0f;
        NavMeshPath currPath = new NavMeshPath();
        NavMesh.CalculatePath(agent.transform.position, target.position, agent.areaMask, currPath);
        for (int i = 0; i < currPath.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(currPath.corners[i], currPath.corners[i + 1]);
        }

        return distance; ;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Minotaur triggered");

        if (other.tag == "Player") // change to AI or player
        {
            print("Player got Reset.");
            player.transform.position = player.startingPos;
            transform.position = startingPos;
        }
        if (other.tag == "AI Opponent" || other.tag == "Opponent")
        {
            print("AI got reset");
            AI.transform.parent.transform.position = AI.startingPos;
            transform.position = startingPos;
        }
    }


}
