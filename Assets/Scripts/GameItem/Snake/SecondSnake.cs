using UnityEngine;

public class SecondSnake : Snake {

	public SecondSnake(PlayGround board, SnakesManger manager, int atColumn): base(board, manager, atColumn) {
	}

	public void OnHandleInput() {
		Vector2Int? translation = KeyUtils.GetBasicDirectionOnASWD();
        if (translation != null) {
            this.Move(translation ?? new Vector2Int(0, 0));
        }
	}
}