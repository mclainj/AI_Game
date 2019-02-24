using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentRollUI : MonoBehaviour
{
    string playerRoll, AIRoll = "";
    string playerText = "Player Rolled:\n";
    string AIText = "\nAI Rolled:\n";
    [SerializeField] Text rollText;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateRollText();
    }

    public void UpdatePlayerRoll(int roll)
    {
        if (roll == 5)
        {
            playerRoll = "Gate";
        } else if (roll == 6)
        {
            playerRoll = "Minotaur";
        }
        else
        {
            playerRoll = roll.ToString();
        }
        UpdateRollText();
    }

    public void UpdateAIRoll(int roll)
    {
        if (roll == 5)
        {
            AIRoll = "Gate";
        } else if (roll == 6)
        {
            AIRoll = "Minotaur";
        }
        else
        {
            AIRoll = roll.ToString();
        }
        UpdateRollText();
    }

    private void UpdateRollText()
    {
        rollText.text = playerText + playerRoll + AIText + AIRoll;
    }
}
