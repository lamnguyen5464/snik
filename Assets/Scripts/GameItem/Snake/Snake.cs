using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Snake: GameItem{
	public PlayGround board { get; private set;}
	private SnakeGameMode manager;
	public int atColumn { get; private set;}
	public SnakeData data {get; private set;}
	private Vector2Int latestDirection;
	private SwipeManager swipeManager;
	public Snake(PlayGround board, SnakeGameMode manager, int atColumn) {
		this.board = board;
		this.atColumn = atColumn;
		this.manager = manager;
		this.Reset();

		swipeManager = new SwipeManager(new SwipeManager.OnSwipeHandler(
			() => { this.Move(Vector2Int.up); },
			() => { this.Move(Vector2Int.down); },
			() => { this.Move(Vector2Int.left); },
			() => { this.Move(Vector2Int.right); }
		));

	}

	public void Reset() {
		Tile tileColor = this.board.colorItems[this.atColumn <= 0 ? 0 : 1].tile;
		this.data = new SnakeData(tileColor, 5, this.atColumn);
	}
	public virtual void OnIncreaseScore() {
		++this.data.score;
		ScoringText.instance.changeScore(this.data.score);
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
		OccupiedType occupiedType = this.checkOccupation(board.tilemap, board.Bounds);
		//Debug.Log("occupiedType: " + occupiedType + " " +dir.x + " " + dir.y);
		switch (occupiedType) {
			case OccupiedType.None:
				this.data.MoveTo(newPosition);
				break;
			case OccupiedType.Built:
				AudioManager.instance.Play("SnakeDown");
				this.board.OnSettleDown(this);
				int scoreBefore = this.data.score;
				this.manager.Reset();
				this.data.score = scoreBefore;
				break;
			case OccupiedType.Crash:
				AudioManager.instance.Play("SnakeCrash");
				this.Reset();
				break;
			default:
				break;
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

	public OccupiedType checkOccupation(Tilemap tilemap, RectInt bounds) {
		Vector3Int head = this.data.nextHead;
		if (head == null) {
			return OccupiedType.None;
		}

		if (manager.IsOccupiedByAllSnakes(head)) {
			return OccupiedType.Crash;
		}

		bool outOfBound = !bounds.Contains((Vector2Int)head);
		bool occupied = tilemap.HasTile(head);

		return (outOfBound || occupied) ? OccupiedType.Built : OccupiedType.None;
	}

	public List<Vector3Int> GetPositionsToSettleDown() {
		Queue<Vector3Int> snakeBody = this.data.position;
		List<Vector3Int> positions = new List<Vector3Int>();
		foreach (var item in snakeBody.ToArray()) {
			positions.Add(item);
		}
		return positions;
	}

	public Vector2Int? OnHandleInput() {
		this.swipeManager.Update();
		Vector2Int? translation = KeyUtils.GetBasicDirectionOnASWD();
		return translation;
	}

	public bool IsOccupied(Vector3Int cell) {
		foreach (var pos in this.GetPositionsToSettleDown()) {
            if (pos.x == cell.x && pos.y == cell.y) {
				return true;
			}
		}

		return false;
	}

}
