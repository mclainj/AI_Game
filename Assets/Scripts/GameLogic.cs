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
        player.GetComponent<PlayerController>().changedTurns = false; // stops mutliple calls of AI turn in player update
        RollDice(player);
        if (diceRoll < 5)
        {
            player.GetComponent<PlayerController>().distanceTravelled = 0f; // reset player dist val
                                                                            // set player dice val
            player.GetComponent<PlayerController>().moveTurn = true; // start player turn
        }
    }

    public void AITurn() // called at end of player turn
    {
        // roll dice
        // set AI dice val
        AI.GetComponent<AI>().changedTurns = false;
        RollDice(AI);
        //AI.GetComponent<AI>().InitializeGateAction();
        //gate.GetComponent<Gate>().ActivateClosestGate(player.transform, finish.transform);
        //gate.GetComponent<Gate>().DeactivateClosestGate(AI.transform, finish.transform);
        if (diceRoll < 5)
        {
            AI.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    private void RollDice(GameObject player)
    {
        System.Random rand = new System.Random();
        diceRoll = rand.Next(1, 6); // generates # from [1,6] | TODO change to 1,7
        print(player.transform.name + " rolled " + diceRoll);
        switch (diceRoll)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                if (player == this.player)
                {
                    player.GetComponent<PlayerController>().distanceRoll = diceRoll;
                } else if (player == AI)
                {
                    AI.GetComponent<AI>().distanceRoll = diceRoll;
                } else
                {
                    Debug.LogError("Invalid player/AI object in RollDice switch statement. Case 1-4");
                }
                break;
            case 5: //gate turn
                if (player == this.player)
                {
                    player.GetComponent<PlayerController>().gateTurn = true;
                }
                else if (player == AI)
                {
                    AI.GetComponent<AI>().gateTurn = true; ;
                }
                else
                {
                    Debug.LogError("Invalid player/AI object in RollDice switch statement. Case gate turn");
                }
                break;
            case 6: 
                // minotaur 
            default:
                Debug.LogError("Error in roll dice default statement.");
                break;
        }


    }

}
