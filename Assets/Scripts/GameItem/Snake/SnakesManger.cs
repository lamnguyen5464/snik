using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakesManger : MonoBehaviour
{
    // Start is called before the first frame update
    private Board board;
    private Snake snake;

    public void Initialize(Board board) {
        this.board = board;
        snake = new Snake(board, true);
    }

    void Start()
    {
        snake = new Snake(board, true);
        snake.OnDraw(board.tilemap);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
