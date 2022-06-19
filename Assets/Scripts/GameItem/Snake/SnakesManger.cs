using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakesManger : MonoBehaviour {
    // Start is called before the first frame update
    private Board board;
    private Snake snake;

    public void Initialize(Board board) {
        this.board = board;
        snake = new Snake(board, true);
    }

    void Start() {
    }

    // Update is called once per frame
    void Update() {
        snake.OnClear(board.tilemap);
        this.HandleInput();
        snake.OnDraw(board.tilemap);
    }

    private void HandleInput() {
        Vector2Int? translation = KeyUtils.GetBasicDirectionOnKey();
        if (translation != null) {
            this.snake.Move(translation ?? new Vector2Int(0, 0));
        }
    }
}
