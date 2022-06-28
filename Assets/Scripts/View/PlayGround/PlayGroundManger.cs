using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGroundManger : MonoBehaviour {
    // Start is called before the first frame update

    private SnakeGameMode gameMode;

    public void Initialize(PlayGround board) {
        // switch gameMode here

        gameMode = new OfflineSingleMode();
        // gameMode = new OnlineMultiMode();
        // gameMode = new OfflineMultiMode();
        gameMode.Initialize(board);

        FindObjectOfType<AudioManager>().Play("background");
    }

    void Start() {
        gameMode.Start();
    }

    // Update is called once per frame
    void Update() {
        gameMode.Update();
    }

    public bool IsOccupiedByAllSnakes(Vector3Int cell) {
        return gameMode.IsOccupiedByAllSnakes(cell);
    }

    public void Reset() {
        gameMode.Reset();
    }

}
