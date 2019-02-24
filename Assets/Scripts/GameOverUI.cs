using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{ 
    [SerializeField] Text gameOverText;

    private void Start()
    {
        gameOverText.text = "";
    }

    public void UpdateVictory()
    {
        print("UpdateVictory called");
        gameOverText.text = "VICTORY";
    }

    public void UpdateDefeat()
    {
        print("update defeat called");
        gameOverText.text = "DEFEAT";
    }
}
