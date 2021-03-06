using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public interface GameItem {
	void Reset();
	void OnDraw(Tilemap tilemap);
	void OnClear(Tilemap tilemap);
	Vector2Int? OnHandleInput();
	OccupiedType checkOccupation(Tilemap tilemap, RectInt bounds);
	List<Vector3Int> GetPositionsToSettleDown();
	void OnIncreaseScore();
}