using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    [SerializeField] private Sprite circle;
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
        spriteRenderer.sprite = circle;
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
    }

    public void SetWhite()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = circle;
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void SetValidSquare()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.color = new Color(0f, 1f, 0f, .4f);
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
