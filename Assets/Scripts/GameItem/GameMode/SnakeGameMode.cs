using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SnakeGameMode {
	void Initialize(PlayGround board);
	void Start();
	void Update();
	bool IsOccupiedByAllSnakes(Vector3Int cell);
	void Reset();
}