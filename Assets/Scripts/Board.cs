using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public List<HappyPiece> happyPieces { get; private set; }
    public HappyPiece happyPiecePrefab;
    public List<TetrominoData> tetrominoData { get; private set; }
    public Tile[] happyTiles;
    public Tile greenTile;
    public Tile orangeTile;
    public Tile purpleTile;
    public Tile redTile;
    public Tile yellowTile;
    public Tile cyanTile;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    private string cyanTileName = "Cyan";
    private string cyanHappyTileName = "happyCyan";
    private string greenTileName = "Green";
    private string greenHappyTileName = "happyGreen";
    private string orangeTileName = "Orange";
    private string orangeHappyTileName = "happyOrange";
    private string purpleTileName = "Purple";
    private string purpleHappyTileName = "happyPurple";
    private string redTileName = "Red";
    private string redHappyTileName = "happyRed";
    private string yellowTileName = "Yellow";
    private string yellowHappyTileName = "happyYellow";

    private int currentPiece;

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private void Awake()
    {
        /*this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        this.happyPieces = new List<HappyPiece>();
        FindHappyPieces();

        for(int i = 0; i < this.tetrominoData.Length; i++)
        {
            this.tetrominoData[i].Init();
        }*/
        Debug.Log("Awake del Board");

    }

    void Start()
    {
        Debug.Log("Start del Board");

        BuildTilemapForLevel();
        this.activePiece = GetComponentInChildren<Piece>();
        this.happyPieces = new List<HappyPiece>();
        this.tetrominoData = new List<TetrominoData>();
        FindHappyPieces();

        BuildTetronimoForLevel();

        SpawnPiece();
    }

    private void BuildTetronimoForLevel()
    {
        int level = GameManager.Instance.currentScreen.Equals(Screen.LEVEL1) ? 1 : 2;
        this.currentPiece = 0;
        if(level == 1)
        {
            this.tetrominoData.Add(new TetrominoData(Tetromino.I, cyanTile, Data.Cells[Tetromino.I], Data.WallKicks[Tetromino.I]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.O, yellowTile, Data.Cells[Tetromino.O], Data.WallKicks[Tetromino.O]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.O, yellowTile, Data.Cells[Tetromino.O], Data.WallKicks[Tetromino.O]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.T, redTile, Data.Cells[Tetromino.T], Data.WallKicks[Tetromino.T]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.S, purpleTile, Data.Cells[Tetromino.S], Data.WallKicks[Tetromino.S]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.L, redTile, Data.Cells[Tetromino.L], Data.WallKicks[Tetromino.L]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.S, purpleTile, Data.Cells[Tetromino.S], Data.WallKicks[Tetromino.S]));
        }else if(level == 2)
        {
            this.tetrominoData.Add(new TetrominoData(Tetromino.I, redTile, Data.Cells[Tetromino.I], Data.WallKicks[Tetromino.I]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.J, cyanTile, Data.Cells[Tetromino.J], Data.WallKicks[Tetromino.J]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.Z, redTile, Data.Cells[Tetromino.Z], Data.WallKicks[Tetromino.Z]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.T, greenTile, Data.Cells[Tetromino.T], Data.WallKicks[Tetromino.T]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.O, orangeTile, Data.Cells[Tetromino.O], Data.WallKicks[Tetromino.O]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.J, cyanTile, Data.Cells[Tetromino.J], Data.WallKicks[Tetromino.J]));
            this.tetrominoData.Add(new TetrominoData(Tetromino.O, orangeTile, Data.Cells[Tetromino.O], Data.WallKicks[Tetromino.O]));
        }
        for (int i = 0; i < this.tetrominoData.Count; i++)
        {
            this.tetrominoData[i].Init();
        }

        Debug.Log(this.tetrominoData.ToString());
    }

    private void BuildTilemapForLevel()
    {
        int level = GameManager.Instance.currentScreen.Equals(Screen.LEVEL1) ? 1 : 2;
        Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
        if (level == 1)
        {
            this.tilemap = tilemaps[0];
            tilemaps[1].gameObject.SetActive(false);
        } else if(level == 2)
        {
            this.tilemap = tilemaps[1];
            tilemaps[0].gameObject.SetActive(false);
        }
    }

    private void FindHappyPieces()
    {
        RectInt bounds = this.Bounds;
        
        for(int row = bounds.yMin;  row < bounds.yMax; row++)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                if (this.tilemap.HasTile(position))
                {
                    Tile tile = this.tilemap.GetTile<Tile>(position);
                    if (tile.ToString().Contains(cyanHappyTileName))
                    {
                        Debug.Log("CYAN en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.CYAN, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }else if (tile.ToString().Contains(greenHappyTileName))
                    {
                        Debug.Log("GREEN en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.GREEN, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                    else if (tile.ToString().Contains(orangeHappyTileName))
                    {
                        Debug.Log("ORANGE en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.ORANGE, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                    else if (tile.ToString().Contains(purpleHappyTileName))
                    {
                        Debug.Log("PURPLE en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.PURPLE, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                    else if (tile.ToString().Contains(redHappyTileName))
                    {
                        Debug.Log("RED en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.RED, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                    else if (tile.ToString().Contains(yellowHappyTileName))
                    {
                        Debug.Log("YELLOW en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.YELLOW, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                }
            }
        }

        Debug.Log("End of FindHappyPieces");
    }

    public void SpawnPiece()
    {
        TetrominoData data = this.tetrominoData[currentPiece];
        currentPiece++;
        if (currentPiece == this.tetrominoData.Count) currentPiece = 0;
        Debug.Log("Current Piece = " + currentPiece);

        this.activePiece.Init(this, this.spawnPosition, data);
        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        }
        else
        {
            Debug.Log("Can't Spawn!");
            GameManager.Instance.ChangeScene("Lose");
        }
    }

    public void Set(Piece piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void CheckHappyPieces()
    {
        List<Vector3Int> positionsToRemove = new List<Vector3Int>();
        List<Vector3Int> positionsToRemoveFromHappyPiece = new List<Vector3Int>();
        List<int> happyIds = new List<int>();
        foreach (HappyPiece happyPiece in happyPieces)
        {
            positionsToRemoveFromHappyPiece.AddRange(GetLinePositions(happyPiece.position, happyPiece.color));
            if (positionsToRemoveFromHappyPiece.Count >= 4)
            {
                happyIds.Add(happyPiece.id);
                positionsToRemove.AddRange(positionsToRemoveFromHappyPiece);
            }
            positionsToRemoveFromHappyPiece.Clear();
        }

        // Remove Happy Pieces
        if (happyIds.Count > 0)
        {
            foreach (int id in happyIds) happyPieces.RemoveAll(happyPiece => happyPiece.id == id);
        }

        // Clear those cells
        foreach (Vector3Int position in positionsToRemove) this.tilemap.SetTile(position, null);

        // Check if the player has won
        if(happyPieces.Count == 0)
        {
            GameManager.Instance.ChangeScene("Win");
        }
    }

    private List<Vector3Int> GetLinePositions(Vector3Int position, Color color)
    {
        List<Vector3Int> positionsToRemove = new List<Vector3Int>();
        // Horizontal
        positionsToRemove.AddRange(SameColorHorizontalNeighbour(1, position, color));
        positionsToRemove.AddRange(SameColorHorizontalNeighbour(-1, position, color));
        if ((positionsToRemove.Count + 1) >= 4) {
            positionsToRemove.Add(position);
            return positionsToRemove;
        } else {
            positionsToRemove.Clear();
            // Vertical
            positionsToRemove.AddRange(SameColorVerticalNeighbour(1, position, color));
            positionsToRemove.AddRange(SameColorVerticalNeighbour(-1, position, color));
            if ((positionsToRemove.Count + 1) >= 4)
            {
                positionsToRemove.Add(position);
                return positionsToRemove;
            }
            else
            {
                return new List<Vector3Int>();
            }
        }
    }

    private List<Vector3Int> SameColorHorizontalNeighbour(int direction, Vector3Int position, Color color)
    {
        List<Vector3Int> positionsWithSameColor = new List<Vector3Int>();
        position.x += direction;
        while (this.tilemap.HasTile(position) && AreSameColor(this.tilemap.GetTile<Tile>(position).ToString(), color)){
            positionsWithSameColor.Add(position);
            position.x += direction;
        }
        return positionsWithSameColor;
    }

    private List<Vector3Int> SameColorVerticalNeighbour(int direction, Vector3Int position, Color color)
    {
        List<Vector3Int> positionsWithSameColor = new List<Vector3Int>();
        position.y += direction;
        while (this.tilemap.HasTile(position) && AreSameColor(this.tilemap.GetTile<Tile>(position).ToString(), color))
        {
            positionsWithSameColor.Add(position);
            position.y += direction;
        }
        return positionsWithSameColor;
    }

    private bool AreSameColor(string tileName, Color color)
    {
        switch (color)
        {
            case Color.CYAN:
                if (tileName.Contains(cyanTileName)) return true;
                else return false;
            case Color.GREEN:
                if (tileName.Contains(greenTileName)) return true;
                else return false;
            case Color.ORANGE:
                if (tileName.Contains(orangeTileName)) return true;
                else return false;
            case Color.PURPLE:
                if (tileName.Contains(purpleTileName)) return true;
                else return false;
            case Color.RED:
                if (tileName.Contains(redTileName)) return true;
                else return false;
            case Color.YELLOW:
                if (tileName.Contains(yellowTileName)) return true;
                else return false;
            default:
                return false;
        }
    }

}
