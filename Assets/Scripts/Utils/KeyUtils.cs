using UnityEngine;

public class KeyUtils {
	public static Vector2Int? GetBasicDirectionOnKey() {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			return Vector2Int.left;
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			return Vector2Int.right;
		}
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			return Vector2Int.up;
		}
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			return Vector2Int.down;
		}

		return null;
	}

	public static Vector2Int? GetBasicDirectionOnArrow() {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			return Vector2Int.left;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			return Vector2Int.right;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			return Vector2Int.up;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			return Vector2Int.down;
		}
		return null;
	}
	public static Vector2Int? GetBasicDirectionOnASWD() {
		if (Input.GetKeyDown(KeyCode.A)) {
			return Vector2Int.left;
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			return Vector2Int.right;
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			return Vector2Int.up;
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			return Vector2Int.down;
		}
		return null;
	}
}
