using UnityEngine;

public class SecondSnake : Snake {

	public SecondSnake(PlayGround board, SnakeGameMode manager, int atColumn): base(board, manager, atColumn) {
	}

	public Vector2Int? OnHandleInput() {
		Vector2Int? translation = KeyUtils.GetBasicDirectionOnASWD();
		return translation;
	}
}