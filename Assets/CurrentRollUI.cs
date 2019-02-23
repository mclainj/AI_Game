using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentRollUI : MonoBehaviour
{
    string playerRoll, AIRoll;
    string playerText = "Player Rolled:\n";
    string AIText = "\nAI Rolled:\n";
    Text rollText;
    
    // Start is called before the first frame update
    void Start()
    {
        rollText = GetComponent<Text>();
        UpdateRollText();
    }

    public void UpdatePlayerRoll(int roll)
    {
        if (roll == 5)
        {
            playerRoll = "Gate";
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
