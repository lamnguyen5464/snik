using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineMultiMode : SnakeGameMode {
    // Start is called before the first frame update
    private PlayGround board;
    private Snake firstSnake;
    private SecondSnake secondSnake;

    public void Initialize(PlayGround board) {
        this.board = board;
        firstSnake = new Snake(board, this, -2);
        secondSnake = new SecondSnake(board, this, 2);
    }

    public void Start() {
    }

    // Update is called once per frame
    public void Update() {
        if (firstSnake == null || secondSnake == null) {
            return;
        }

        firstSnake.OnClear(board.tilemap);
        secondSnake.OnClear(board.tilemap);

        Vector2Int? firstSnakeInput = firstSnake.OnHandleInput();
        if (firstSnakeInput != null) {
            firstSnake.Move(firstSnakeInput ?? new Vector2Int(0, 0));
            AudioManager.instance.PlayRandomNotes();
        }
        Vector2Int? secondSnakeInput = secondSnake.OnHandleInput();
        if (secondSnakeInput != null) {
            secondSnake.Move(secondSnakeInput ?? new Vector2Int(0, 0));
            AudioManager.instance.PlayRandomNotes();
        }


        firstSnake.OnDraw(board.tilemap);
        secondSnake.OnDraw(board.tilemap);

    }
    public bool IsOccupiedByAllSnakes(Vector3Int cell) {
        return (this.firstSnake.IsOccupied(cell) || this.secondSnake.IsOccupied(cell));
    }

    public void Reset() {
        this.firstSnake.Reset();
        this.secondSnake.Reset();
    }

}
