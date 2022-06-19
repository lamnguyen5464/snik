using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class SnakeData {
	public Tile tile {get; private set;}
	private int length;
	public Queue<Vector3Int> position {get; private set;}
	public Vector3Int head {get; private set;}
	public Vector3Int nextHead;

	public SnakeData(Tile tile, int length, int atColumn) {
		this.tile = tile;
		this.length = length;
		position = new Queue<Vector3Int>();

		Vector3Int basePosition = new Vector3Int(atColumn, 9, 0);
		while (length > 0) {
			head = new Vector3Int(basePosition.x, basePosition.y, basePosition.z);
			position.Enqueue(head);
			basePosition.y = basePosition.y - 1;
			length = length - 1;
		}
	}

	public void MoveTo(Vector3Int newHead) {
		head = newHead;
		position.Enqueue(head);
		position.Dequeue();
	}
}