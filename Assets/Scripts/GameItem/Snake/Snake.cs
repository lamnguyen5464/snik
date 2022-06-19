using UnityEngine;
using UnityEngine.Tilemaps;

public class Snake: GameItem{
	public Board board { get; private set;}
	public SnakeData data {get; private set;}

	public  Snake(Board board, bool isMySnake) {
		this.board = board;
		Tile tileColor = board.colorItems[isMySnake ? 0 : 1].tile;
		this.data = new SnakeData(tileColor, 4);
	}

	private void Move(Vector2Int dir) {
		Vector3Int newPosition = this.data.head;
        newPosition.x += dir.x;
        newPosition.y += dir.y;
	}

	public void OnDraw(Tilemap tilemap) {
		foreach(var pos in this.data.position.ToArray()) {
			tilemap.SetTile(pos, this.data.tile);
		}
	}

	public void OnClear(Tilemap tilemap) {
		foreach(var pos in this.data.position.ToArray()) {
			tilemap.SetTile(pos, null);
		}
	}
}