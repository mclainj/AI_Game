using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    [SerializeField] int distanceRoll = 5;
    public NavMeshAgent agentAI;
    public NavMeshAgent agentPlayer;
    public GameObject finish;
    public GameObject pastMarker;

    private Transform lastPos;

    //GameLogic manager;
    [SerializeField] GameLogic manager;

    private void Start()
    {
        agentAI.isStopped = true;
        //manager = GetComponent<GameLogic>();
        //lastPos = agentAI.transform;
        lastPos = pastMarker.transform;
    }

    private void Update()
    {
        //print("Dist between AI & Player: " + getPathDistance(agentAI, agentPlayer.transform));
        //print("Dist between AI & target: " + getPathDistance(agentAI, finish.transform));
        //print("TEST " + getPathDistance(agentAI, test.transform));
        //print("Dist between Player & target: " + getPathDistance(agentPlayer, finish.transform));
        Travel();

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
        if (other.tag == "Finish")
        {
            print("Game Over. You Lose.");
        }
    }

}
