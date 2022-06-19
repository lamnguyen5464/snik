using UnityEngine;
using UnityEngine.Tilemaps;


interface GameItem {
	void OnDraw(Tilemap tilemap);
	void OnClear(Tilemap tilemap);
}