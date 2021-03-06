﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject AI;
    //Minotaur minotaur;
    [SerializeField] GameObject finish;
    [SerializeField] GameObject gate; //gate1 reference
    public NavMeshSurface stage; //surface.BuildNavMesh() to rebuild navMesh

    private int diceRoll;

    CurrentRollUI currentRollUI;
    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        // Roll Die
        //minotaur = FindObjectOfType<Minotaur>();
        currentRollUI = FindObjectOfType<CurrentRollUI>();
        PlayerTurn(); // start with player turn
    }


    public void PlayerTurn() // called at end of AI turn
    {
        // roll dice
        player.GetComponent<PlayerController>().changedTurns = false; // stops mutliple calls of AI turn in player update
        RollDice(player);
        currentRollUI.UpdatePlayerRoll(diceRoll);
        if (diceRoll < 5)
        {
            player.GetComponent<PlayerController>().distanceTravelled = 0f; // reset player dist val
                                                                            // set player dice val
            player.GetComponent<PlayerController>().moveTurn = true; // start player turn
        }
        /*else if  (diceRoll == 6)
        {
            minotaur.playerMinotaur = true;
        }*/
    }

    public void AITurn() // called at end of player turn
    {
        AI.GetComponent<AI>().changedTurns = false;
        RollDice(AI);
        currentRollUI.UpdateAIRoll(diceRoll);
        if (diceRoll < 5)
        {
            AI.GetComponent<NavMeshAgent>().isStopped = false;
        }
        /*else if (diceRoll == 6)
        {
            minotaur.AIMinotaur = true;
        }*/
    }

    private void RollDice(GameObject player)
    {
        diceRoll = rand.Next(1, 6); // generates # from [1,6] | todo change to (1,7) when minotaur implemented
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
                print(player.name + " rolled minotaur");
                break;
            default:
                Debug.LogError("Error in roll dice default statement.");
                break;
        }


    }

}
