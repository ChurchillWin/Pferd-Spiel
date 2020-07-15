using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    AudioClip[] moveSounds;
    AudioSource audioPlayer;
    public delegate void MoveAction();
    public static event MoveAction OnMoved;
    public int x { get; set; }
    public int y { get; set; }

    private void OnEnable()
    {
        UiController.OnReset += Reset;
    }
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public bool Move(int targetX, int targetY, Tile targetTile)
    {
        //Knight moves logic
        if (
            (x - 2 == targetX && y - 1 == targetY) ||
            (x - 2 == targetX && y + 1 == targetY) ||
            (x + 2 == targetX && y + 1 == targetY) ||
            (x + 2 == targetX && y - 1 == targetY) ||
            (x - 1 == targetX && y + 2 == targetY) ||
            (x - 1 == targetX && y - 2 == targetY) ||
            (x + 1 == targetX && y + 2 == targetY) ||
            (x + 1 == targetX && y - 2 == targetY)
           )
        {
            transform.position = targetTile.transform.position;
            x = targetX;
            y = targetY;
            OnMoved?.Invoke();
            audioPlayer.PlayOneShot(moveSounds[UnityEngine.Random.Range(0, moveSounds.Length)]);
            return true;
        }
        else
            return false;
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
