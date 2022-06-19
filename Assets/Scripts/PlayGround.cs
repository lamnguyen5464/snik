using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PlayGround : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public SnakesManger snakesManger { get; private set; }
    public Vector2Int boardSize;
    public TileColorItem[] colorItems;

    public RectInt Bounds {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        snakesManger = GetComponentInChildren<SnakesManger>();

        snakesManger.Initialize(this);
   }

    private void Start()
    {
    }

    public void OnSettleDown(GameItem gameItem) {
		List<Vector3Int> positions = gameItem.GetPositionsToSettleDown();
        foreach(var pos in positions) {
            Tile defaultBrick = colorItems[3].tile;
            tilemap.SetTile(pos, defaultBrick);
            // check for + score
            int row = pos.y;
            if (this.IsLineFull(row)){
                this.LineClear(row);
            }
        }
    }

     public bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            // The line is not full if a tile is missing
            if (!tilemap.HasTile(position)) {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row)
    {
        RectInt bounds = Bounds;

        // Clear all tiles in the row
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            tilemap.SetTile(position, null);
        }

        // Shift every row above down one
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, above);
            }

            row++;
        }
    }

}
