using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public interface GameItem {
	void Reset();
	void OnDraw(Tilemap tilemap);
	void OnClear(Tilemap tilemap);
	bool OnHandleInput();
	OccupiedType checkOccupation(Tilemap tilemap, RectInt bounds);
	List<Vector3Int> GetPositionsToSettleDown();
}