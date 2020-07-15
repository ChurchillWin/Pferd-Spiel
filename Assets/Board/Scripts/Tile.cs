using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameBoard gameBoard;
    private Player player;
    private bool colored = false;
    //properties
    private Color _tileColor;
    public int id { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public Color tileColor 
    {
        private get 
        { 
            if (_tileColor != Color.clear ) 
            { return _tileColor; } 
            else 
            { return Color.white; } 
        }
        set { _tileColor = value; }
    }
    public void Initialize(int x, int y, int id)
    {
        this.x = x;
        this.y = y;
        this.id = id;
    }
    private void OnEnable()
    {
        UiController.OnReset += Reset;
    }
    void Start()
    {
        gameBoard = FindObjectOfType<GameBoard>();
        gameObject.GetComponent<SpriteRenderer>().color = tileColor;
        player = FindObjectOfType<Player>();
    }

    private void OnMouseDown()
    {
        if(player.Move(x, y, this))
        {
            colored = !colored;
            SwapColors();
        }
    }
    //Swap colors and increment colored tiles and check if game is won
    private void SwapColors()
    {
        if (colored)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            GameBoard.coloredTiles++;
            gameBoard.WinGame();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = tileColor;
            GameBoard.coloredTiles--;
        }

    }
    private void Reset()
    {
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        UiController.OnReset -= Reset;
    }

}
