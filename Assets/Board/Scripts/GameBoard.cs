//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    private void OnEnable()
    {
        UiController.OnReset += Reset;
    }
    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private Player playerPrefab;
    public Tile[] tiles;
    private int counter;
    public static int coloredTiles;
    [SerializeField]
    [Tooltip("Default margin '2'")] private float margin = 1.8f;

    public delegate void WinAction();
    public static event WinAction OnGameWon;
    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        //Random player position
        int randX, randY;
        randX = Random.Range(0, 8); randY = Random.Range(0, 8);
        counter = 0;
        for(int i = 0;i < 8;i++)
        {
            //calculate next position based on iteration of the current loop
            float yAxis = i / margin;
            for(int j = 0;j < 8; j++)
            {
                //Spawn player on random position 
                if(j == randX && i == randY)
                {
                    SpawnPlayer(j, i);
                }
                float xAxis = j / margin;
                //instantiate and position new tile
                Tile newTile = Instantiate(tilePrefab, this.transform, false);
                newTile.transform.localPosition = new Vector2(xAxis, yAxis); 
                //initialize x, y coordinates and id of new tile
                newTile.Initialize(j, i, counter);
                //count tiles from 0
                counter++;
                //Chess board pattern
                if ((i % 2 == 1 && j % 2 == 1) || (i % 2 == 0 && j % 2 == 0))
                {
                    newTile.tileColor = Color.black;
                }
            }
        }
    }
    private void SpawnPlayer(int x, int y)
    {
        Player newPlayer = Instantiate(playerPrefab, this.transform, false);
        newPlayer.transform.localPosition = new Vector2(x / margin, y / margin);
        newPlayer.Initialize(x, y);
    }
    public void WinGame()
    {
        if (coloredTiles == counter)
        {
            Debug.Log("Congrats!");
        }
    }
    private void Reset()
    {
        Setup();
    }
    private void OnDisable()
    {
        OnGameWon?.Invoke();
    }
}
