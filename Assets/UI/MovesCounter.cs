using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesCounter : MonoBehaviour
{
    private Text counter;
    private int moves = 0;

    void Start()
    {
        counter = GetComponent<Text>();
        counter.text = "Moves: " + moves;
    }
    private void OnEnable()
    {
        Player.OnMoved += Increment;
        UiController.OnReset += Reset;
    }

    private void Increment()
    {
        moves++;
        counter.text = "Moves: " + moves;

    }
    private void Reset()
    {
        moves = 0;
        counter.text = "Moves: " + moves;
    }
    private void OnDisable()
    {
        Player.OnMoved -= Increment;
        UiController.OnReset -= Reset;
    }

    

}
