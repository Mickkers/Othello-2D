using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverUIHandler : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] TMP_Text[] textUI;
    [SerializeField] Image image;

    private void OnEnable()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        if (gameManager.GetBlackScore() > gameManager.GetWhiteScore())
        {
            textUI[1].text = "Black Wins";
        }
        else if (gameManager.GetBlackScore() < gameManager.GetWhiteScore())
        {
            textUI[1].text = "White Wins";
        }
        else
        {
            textUI[1].text = "Game is a draw";
        }

        textUI[2].text = "White Score: " + gameManager.GetWhiteScore();
        textUI[3].text = "Black Score: " + gameManager.GetBlackScore();
    }

    public void PlayAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
