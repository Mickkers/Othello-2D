using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject boardPiece;
    [SerializeField] private GameObject pieces;
    private char[,] board = new char[8, 8];
    private BoardPiece[,] gameBoard = new BoardPiece[8,8];
    private BoardPiece[] boardPieces;
    private const char blank = 'x';
    private const char black = 'b';
    private const char white = 'w';
    private char currMove = black;
    private int blackScore = 0;
    private int whiteScore = 0;
    private bool gameOver;
    private bool boardSet = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                board[i, j] = blank;
                Object.Instantiate(boardPiece, Vector3.zero, Quaternion.identity, pieces.transform);
            }
        }
        board[3, 3] = white;
        board[3, 4] = black;
        board[4, 3] = black;
        board[4, 4] = white;

        SetGameBoard();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            UpdateGameBoard();
        }
        else
        {
            gameoverUI.SetActive(true);
        }
    }

    public void PlayMove(int row, int col)
    {
        if (!IsValidMove(row, col, currMove)) return;

        board[row, col] = currMove;
        char opp = currMove == black ? white : black;

        int[,] directions = new int[,] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dRow = directions[i, 0];
            int dCol = directions[i, 1];

            if (row + dRow >= 0 &&
                row + dRow < board.GetLength(0) &&
                col + dCol >= 0 &&
                col + dCol < board.GetLength(1) &&
                board[row + dRow, col + dCol] == opp)
            {
                int j = row + dRow;
                int k = col + dCol;
                while (j >= 0 &&
                    j < board.GetLength(0) &&
                    k >= 0 &&
                    k < board.GetLength(1))
                {
                    if (board[j, k] == blank) break;

                    if (board[j, k] == currMove)
                    {
                        while (j != row || k != col)
                        {
                            j -= dRow;
                            k -= dCol;
                            board[j, k] = currMove;
                        }
                        break;
                    }

                    j += dRow;
                    k += dCol;
                }
            }
        }

        currMove = opp;
    }



    public bool IsValidMove(int row, int col, char pla)
    {
        char opp = pla == black ? white : black;
        int[,] directions = new int[,] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };

        if (board[row, col] != blank)
        {
            return false;
        }

        for(int i = 0; i < directions.GetLength(0); i++)
        {
            int dRow = directions[i, 0];
            int dCol = directions[i, 1];

            if (row + dRow >= 0 &&
                row + dRow < board.GetLength(0) &&
                col + dCol >= 0 &&
                col + dCol < board.GetLength(1) &&
                board[row + dRow, col + dCol] == opp)
            {
                int j = row + dRow;
                int k = col + dCol;
                while (j >= 0 &&
                    j < board.GetLength(0) &&
                    k >= 0 &&
                    k < board.GetLength(1))
                {
                    if (board[j, k] == blank) break;
                    
                    if (board[j, k] == pla) return true;

                    j += dRow;
                    k += dCol;
                }
            }
        }

        return false;
    }

    private void UpdateGameBoard()
    {
        if(!boardSet)
        {
            return;
        }
        int whiteCount = 0;
        int blackCount = 0;
        int validCount = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(gameBoard[i, j] == null)
                {
                    //Debug.Log("Null " + i + " " + j);
                    continue;
                }
                if (board[i, j] == white)
                {
                    gameBoard[i, j].SetWhite();
                    whiteCount++;
                }
                else if (board[i, j] == black)
                {
                    gameBoard[i, j].SetBlack();
                    blackCount++;
                }
                else if (IsValidMove(i, j, currMove))
                {
                    gameBoard[i, j].SetValidSquare();
                    validCount++;
                }
                else
                {
                    gameBoard[i, j].SetUnvalidSquare();
                }
            }
        }

        blackScore = blackCount;
        whiteScore = whiteCount;

        if(validCount == 0)
        {
            gameOver = true;
        }
    }

    private void SetGameBoard()
    {
        boardPieces = FindObjectsOfType(typeof(BoardPiece)) as BoardPiece[];
        int row = 0;
        int col = 0;
        foreach(BoardPiece piece in boardPieces)
        {
            piece.SetRowCol(row, col);

            gameBoard[row, col] = piece;

            if(col == 7)
            {
                col = 0;
                row++;
            } else
            {
                col++;
            }
        }
        boardSet = true;
    }

    public char GetCurrPlayer()
    {
        return currMove;
    }

    public int GetWhiteScore()
    {
        return whiteScore;
    }

    public int GetBlackScore()
    {
        return blackScore;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }
}
