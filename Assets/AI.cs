using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    [SerializeField] int distanceRoll = 5;
    public NavMeshAgent agentAI;
    public NavMeshAgent agentPlayer;
    private int gateMask;

    public GameObject finish;
    public GameObject pastMarker;

    private Transform lastPos;

    //GameLogic manager;
    [SerializeField] GameLogic manager;

    public bool moveRoll = true;
    public bool gateRoll = false;
    //public bool moveRoll = true;
    [SerializeField] GameObject debugMarker;

    private void Start()
    {
        agentAI.isStopped = true;
        //manager = GetComponent<GameLogic>();
        //lastPos = agentAI.transform;
        lastPos = pastMarker.transform;
        gateMask = 1 << NavMesh.GetAreaFromName("Gate");
    }

    private void Update()
    {
        //print("Dist between AI & Player: " + getPathDistance(agentAI, agentPlayer.transform));
        //print("Dist between AI & target: " + getPathDistance(agentAI, finish.transform));
        //print("TEST " + getPathDistance(agentAI, test.transform));
        //print("Dist between Player & target: " + getPathDistance(agentPlayer, finish.transform));
        Travel();
        if (gateRoll)
        {
            GateAction();
        }
    }

    private void Travel() // todo implement player avoidence
    {
        if (!agentAI.hasPath)
        {
            agentAI.SetDestination(finish.transform.position);
        }
        else
        {
            //print(getPathDistance(agentAI, lastPos) + " / " + distanceRoll);
            if (getPathDistance(agentAI, lastPos) >= distanceRoll)
            {
                //print("STOP");
                agentAI.isStopped = true;
                pastMarker.transform.position = agentAI.transform.position;
                lastPos = pastMarker.transform;
                manager.GetComponent<GameLogic>().PlayerTurn();
            }
        }
    }

    private float getPathDistance(NavMeshAgent agent, Transform target) 
    {
        float distance = 0f;
        NavMeshPath currPath = new NavMeshPath();
        NavMesh.CalculatePath(agent.transform.position, target.position, agent.areaMask, currPath);
        for(int i = 0; i < currPath.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(currPath.corners[i], currPath.corners[i + 1]);
        }

        return distance; ;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("AI triggered");
        if (other.tag == "Finish")
        {
            print("Game Over. You Lose.");
        }
    }

    public void InitializeGateAction()
    {
        moveRoll = false;
        gateRoll = true;
        // activate gate to block player:
        agentPlayer.isStopped = false;
        agentPlayer.SetDestination(finish.transform.position);

    }


    private void GateAction() // todo fix occasional incorrect gate assignment
    {
        NavMeshHit hit;
        // find closest gate on path for player
        if(!agentPlayer.SamplePathPosition(NavMesh.AllAreas, 1000f, out hit))
        {
            if((hit.mask & gateMask) != 0)
            {
                // gate detected along path
                print("GATE DETECTED ALONG PATH");
                Collider[] intersectingObjects = Physics.OverlapSphere(hit.position, 3f);
                if(intersectingObjects.Length != 0)
                {
                    //GameObject gate = intersectingGate[0].transform.gameObject;
                    GameObject gate = identifyGate(intersectingObjects);
                    if (gate != null)
                        print("Intersecting Object name: " + gate.name);
                    debugMarker.transform.position = hit.position;
                    /*if (gate.name == "Gate Frame" || gate.name == "Gate Frame (1)")
                    {
                        gate = gate.transform.parent.gameObject; // update gate to parent
                        GameObject gateShield = gate.transform.GetChild(0).gameObject; // get child shield
                        gateShield.SetActive(true);
                        //gateRoll = false;
                    }*/
                    if (gate != null)
                    {
                        GameObject gateShield = gate.transform.GetChild(0).gameObject;
                        gateShield.SetActive(true);
                        gateRoll = false;
                    }
                }
            }
        }
        agentPlayer.isStopped = true;
    }


    private GameObject identifyGate(Collider[] intersectingObjects)
    {
        GameObject collidedObj;
        foreach (Collider c in intersectingObjects) {
            collidedObj = c.transform.gameObject;
            if (collidedObj.transform.name == "Gate Frame")
            {
                return collidedObj.transform.parent.gameObject;
            }
        }
        return null;
    }

    public void ActivateClosestGate()
    {

    }

}
