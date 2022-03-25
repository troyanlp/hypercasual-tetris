using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z
}

[System.Serializable]
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile tile;
    public Vector2Int[] cells { get; private set; }
    public Vector2Int[,] wallKicks { get; private set; }

    public TetrominoData(Tetromino tetromino, Tile tile, Vector2Int[] cells, Vector2Int[,] wallKicks)
    {
        this.tetromino = tetromino;
        this.tile = tile;
        this.cells = cells;
        this.wallKicks = wallKicks;
    }

    public void Init()
    {
        this.cells = Data.Cells[this.tetromino];
        this.wallKicks = Data.WallKicks[this.tetromino];
    }

}