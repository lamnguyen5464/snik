using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Snake: GameItem{
	public Board board { get; private set;}
	public bool isMySnake { get; private set;}
	public SnakeData data {get; private set;}
	private Vector2Int latestDirection;
	public Snake(Board board, bool isMySnake) {
		this.board = board;
		this.isMySnake = isMySnake;
		this.Reset();
	}

	public void Reset() {
		Tile tileColor = this.board.colorItems[this.isMySnake ? 0 : 1].tile;
		this.data = new SnakeData(tileColor, 5);
	}

	public void Move(Vector2Int dir) {
		bool isEmpty = dir.x == 0 && dir.y == 0;
		bool isConflict = latestDirection != null && (latestDirection.x + dir.x) == 0 && (latestDirection.y + dir.y) == 0;
		if (isEmpty || isConflict) {
			return;
		}

		latestDirection = dir;

		Vector3Int newPosition = this.data.head;
        newPosition.x += dir.x;
        newPosition.y += dir.y;
		this.data.nextHead = newPosition;
		if (this.IsValidPosition(board.tilemap, board.Bounds)){
			this.data.MoveTo(newPosition);
		} else {
			this.board.OnSettleDown(this);
		}
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

	public bool IsValidPosition(Tilemap tilemap, RectInt bounds) {
		Vector3Int head = this.data.nextHead;
		bool outOfBound = !bounds.Contains((Vector2Int)head);
		bool occupied = tilemap.HasTile(head);

		return (!outOfBound && !occupied);
	}

	public List<Vector3Int> GetPositionsToSettleDown() {
		Queue<Vector3Int> snakeBody = this.data.position;
		List<Vector3Int> positions = new List<Vector3Int>();
		while (snakeBody.Count > 0) {
			Vector3Int pos = snakeBody.Dequeue();
			positions.Add(pos);
		}
		return positions;
	}
}