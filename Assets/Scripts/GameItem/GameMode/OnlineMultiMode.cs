using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineMultiMode : SnakeGameMode {
    // Start is called before the first frame update
    private PlayGround board;
    private bool isStarted;
    private int mySnake = 0;
    private Snake firstSnake;
    private Snake secondSnake;

    private Vector2Int? newFirstSnakeTranslation = null;
    private Vector2Int? newSecondSnakeTranslation = null;

    public void Initialize(PlayGround board) {
        this.board = board;
        this.isStarted = false;
        firstSnake = new Snake(board, this, -2);
        secondSnake = new Snake(board, this, 2);
		Profile.getInstance().nickName = "user_01";
    }

    public void Start() {
        this.emitStartSignal();

        Action<object, WebSocketSharp.MessageEventArgs> handler = (sender, eventData) => {
			Debug.Log("From server: "  +eventData.Data);

            PayloadWrapper<StartSignalData> startSignalData= PayloadWrapper<StartSignalData>.FromString<StartSignalData>(eventData.Data);
            if (startSignalData.isValid()) {
                StartSignalData data = startSignalData.GetData();
                Profile.getInstance().clientId = data.clientId;
                if (data.clientIds.Length > 0) {
                    mySnake = (data.clientId == data.clientIds[0]) ? 0 : 1;
                }
                this.isStarted = true;
		        this.emitNewCoordinate(new Vector2Int(0, 0));
                return;
            }

            PayloadWrapper<OnMoveData> onMovePayload = PayloadWrapper<OnMoveData>.FromString<OnMoveData>(eventData.Data);
            if (onMovePayload.isValid()) {
                OnMoveData data =  onMovePayload.GetData();
				OnMoveData.UserItem[] items = data.items;
                
                // ScoringText.name_1.text = "Datttt";

                foreach (var item in items){
				    Coordinate2D newPos = item.position;
                    Vector3Int oldPos = item.id == 0 ? firstSnake.data.head : secondSnake.data.head;
                    if (newPos == null || oldPos == null) {
                        Debug.Log(" nulll ");
                    }
                        Debug.Log(oldPos.x + "-" + oldPos.y + " old " + mySnake);
                        Debug.Log(newPos.x + "-" + newPos.y +  " new " + mySnake);
                    int deltaX = newPos.x - oldPos.x;
                    int deltaY = newPos.y - oldPos.y;

                    if (item.id == 0) {
                        newFirstSnakeTranslation = new Vector2Int(deltaX, deltaY);
                    } else if (item.id == 1) {
                        newSecondSnakeTranslation = new Vector2Int(deltaX, deltaY);
                    }
                    Debug.Log("delta: " + deltaX + "-" + deltaY + " " + mySnake);

                }
                return;
           }

        };
        SocketClient.addHandler(handler);
    }

    // Update is called once per frame
    public void Update() {
        if (firstSnake == null || secondSnake == null || !isStarted) {
            return;
        }

        Vector2Int? move = KeyUtils.GetBasicDirectionOnKey();
        if (move != null) {
            emitNewCoordinate(move ?? new Vector2Int(0, 0));
        }

        if (newFirstSnakeTranslation != null) {
            firstSnake.OnClear(board.tilemap);
            firstSnake.Move(newFirstSnakeTranslation ?? new Vector2Int(0, 0));
            newFirstSnakeTranslation = null;
            firstSnake.OnDraw(board.tilemap);
        }
        
        if (newSecondSnakeTranslation != null) {
            secondSnake.OnClear(board.tilemap);
            secondSnake.Move(newSecondSnakeTranslation ?? new Vector2Int(0, 0));
            newSecondSnakeTranslation = null;
            secondSnake.OnDraw(board.tilemap);
        }

    }

    private void emitNewCoordinate(Vector2Int translation) {
        Vector3Int pos = mySnake == 0 ? firstSnake.data.head : secondSnake.data.head;
        int newX = pos.x + translation.x;
        int newY = pos.y + translation.y;
        //     Debug.Log("Second : " +secondSnake.data.head.x + " " +secondSnake.data.head.y);
        // Debug.Log("new: " + newX + " " + newY);
        NewCoordinateData data = new NewCoordinateData(Profile.getInstance().nickName, newX, newY);
        PayloadWrapper<NewCoordinateData> payloadData = PayloadWrapper<NewCoordinateData>.FromData<NewCoordinateData>(data);
        SocketClient.send(payloadData.GetPayload());
    }

    private void emitStartSignal() {
        StartSignalData signal = new StartSignalData();
        signal.nickName = Profile.getInstance().nickName;
        PayloadWrapper<StartSignalData> payloadData = PayloadWrapper<StartSignalData>.FromData<StartSignalData>(signal);
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
