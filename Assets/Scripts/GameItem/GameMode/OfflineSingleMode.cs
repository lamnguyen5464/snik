using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineSingleMode : SnakeGameMode {
    // Start is called before the first frame update
    private PlayGround board;
    private Snake firstSnake;
    Vector2Int firstSnakeInput;
    float countDelay = 0;
    bool isLose;

    public void Initialize(PlayGround board) {
        this.board = board;
        firstSnake = new Snake(board, this, 0);
        firstSnakeInput = new Vector2Int(0, -1);
        isLose = false;
        ScoringText.instance.applySingleMode();
        ScoringText.instance.applyOfflineMode();
    }

    public void Start() {
    }

    // Update is called once per frame
    public void Update() {
        if (firstSnake == null) {
            return;
        }

        if (isLose) {
            //Debug.Log("Game over!");
            return;
        }

        countDelay += Time.deltaTime;

        firstSnake.OnClear(board.tilemap);

        firstSnakeInput = firstSnake.OnHandleInput() ?? firstSnakeInput;

        if (countDelay > 0.2) {
            countDelay = 0;
            firstSnake.Move(firstSnakeInput);
            AudioManager.instance.PlayRandomNotes();
        }

        firstSnake.OnDraw(board.tilemap);

    }
    public bool IsOccupiedByAllSnakes(Vector3Int cell) {
        return this.firstSnake.IsOccupied(cell);
    }

    public void Reset() {
        Vector3Int head = this.firstSnake.data.head;
        if (head.x == 0 && head.y == 5) {
            this.isLose = true;
        }
        firstSnakeInput = new Vector2Int(0, -1);
        this.firstSnake.Reset();
    }

}
