using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    [SerializeField] private Sprite square;
    [SerializeField] private Sprite white;
    [SerializeField] private Sprite black;
    [SerializeField] private int row;
    [SerializeField] private int column;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    public void SetRowCol(int rows, int cols)
    {
        row = rows;
        column = cols;
        SetPosition();
    }
    private void SetPosition()
    {
        float posx = row - 8.5f;
        float posy = 3.5f - column;
        gameObject.transform.position = new Vector2(posx, posy);
    }

    public void SetBlack()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = black;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void SetWhite()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = white;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void SetValidSquare()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = square;
        spriteRenderer.color = new Color(0f, .6f, 0f, .6f);
    }

    public void SetUnvalidSquare()
    {
        if(spriteRenderer == null) return;
        spriteRenderer.color = new Color(0f, 1f, 0f, 0);
    }

    private void OnMouseDown()
    {
        gameManager.PlayMove(row, column);
    }
}
