using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameplayUIHandler : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] TMP_Text[] textUI;

    private void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        SetTurnUI();
        SetScoreUI();
    }

    private void SetScoreUI()
    {
        textUI[1].text = "Black: " + gameManager.GetBlackScore();
        textUI[2].text = "White: " + gameManager.GetWhiteScore();
    }

    private void SetTurnUI()
    {
        if (gameManager.GetGameOver())
        {
            return;
        }
        else if (gameManager.GetCurrPlayer() == 'b')
        {
            textUI[0].text = "Black's Turn";
        }
        else
        {
            textUI[0].text = "White's Turn";
        }
    }
}
