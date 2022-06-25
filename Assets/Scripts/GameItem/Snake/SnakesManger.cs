using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakesManger : MonoBehaviour {
    // Start is called before the first frame update
    private PlayGround board;
    private Snake firstSnake;
    private SecondSnake secondSnake;

    private Vector2Int? newFirstSnakeTranslation = null;

    public void Initialize(PlayGround board) {
        this.board = board;
        firstSnake = new Snake(board, this, 2);
        secondSnake = new SecondSnake(board, this, -2);
    }

    void Start() {
        this.emitStartSignal();

        Action<object, WebSocketSharp.MessageEventArgs> handler = (sender, eventData) => {
            PayloadWrapper<OnMoveData> onMovePayload = PayloadWrapper<OnMoveData>.FromString<OnMoveData>(eventData.Data);
            if (onMovePayload.isValid()) {
                OnMoveData data =  onMovePayload.GetData();
                int deltaX = data.firstX - firstSnake.data.head.x;
                int deltaY = data.firstY - firstSnake.data.head.y;
                newFirstSnakeTranslation = new Vector2Int(deltaX, deltaY);
            }

        };
        SocketClient.addHandler(handler);
    }

    // Update is called once per frame
    void Update() {
        if (firstSnake == null || secondSnake == null) {
            return;
        }

        firstSnake.OnClear(board.tilemap);
        secondSnake.OnClear(board.tilemap);

        Vector2Int? firstSnakeMove = firstSnake.OnHandleInput();
        if (firstSnakeMove != null) {
            emitNewCoordinate(firstSnakeMove ?? new Vector2Int(0, 0));
        }

        if (newFirstSnakeTranslation != null) {
            firstSnake.Move(newFirstSnakeTranslation ?? new Vector2Int(0, 0));
            newFirstSnakeTranslation = null;
        }

        secondSnake.OnHandleInput();

        firstSnake.OnDraw(board.tilemap);
        secondSnake.OnDraw(board.tilemap);

    }

    public void emitNewCoordinate(Vector2Int translation) {
        Vector3Int pos = firstSnake.data.head;
        NewCoordinateData data = new NewCoordinateData("user_01", pos.x + translation.x, pos.y + translation.y);
        PayloadWrapper<NewCoordinateData> payloadData = PayloadWrapper<NewCoordinateData>.FromData<NewCoordinateData>(data);
        SocketClient.send(payloadData.GetPayload());
    }

    public void emitStartSignal() {
        PayloadWrapper<StartSignalData> payloadData = PayloadWrapper<StartSignalData>.FromData<StartSignalData>(new StartSignalData());
        SocketClient.send(payloadData.GetPayload());
    }

    public bool IsOccupiedByAllSnakes(Vector3Int cell) {
        return (this.firstSnake.IsOccupied(cell) || this.secondSnake.IsOccupied(cell));
    }

    public void Reset() {
        this.firstSnake.Reset();
        this.secondSnake.Reset();
    }

}
