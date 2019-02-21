using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject AI;
    [SerializeField] GameObject finish;
    [SerializeField] GameObject gate; //gate1 reference
    public NavMeshSurface stage; //surface.BuildNavMesh() to rebuild navMesh

    private int diceRoll;

    // Start is called before the first frame update
    void Start()
    {
        // Roll Die
        diceRoll = 5;
        PlayerTurn();
    }


    public void PlayerTurn() // called at end of AI turn
    {
        // roll dice
        player.GetComponent<PlayerController>().distanceTravelled = 0f; // reset player dist val
        // set player dice val
        player.GetComponent<PlayerController>().isTurn = true; // start player turn
    }

    public void AITurn() // called at end of player turn
    {
        // roll dice
        // set AI dice val
        //AI.GetComponent<AI>().InitializeGateAction();
        gate.GetComponent<Gate>().ActivateClosestGate(player.transform, finish.transform);
        gate.GetComponent<Gate>().DeactivateClosestGate(AI.transform, finish.transform);
        AI.GetComponent<NavMeshAgent>().isStopped = false;
    }


}
