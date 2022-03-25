using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public List<HappyPiece> happyPieces { get; private set; }
    public HappyPiece happyPiecePrefab;
    public TetrominoData[] tetrominoData;
    public Tile[] happyTiles;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    private string cyanTile = "Cyan";
    private string cyanHappyTile = "happyCyan";
    private string greenTile = "Green";
    private string greenHappyTile = "happyGreen";
    private string orangeTile = "Orange";
    private string orangeHappyTile = "happyOrange";
    private string purpleTile = "Purple";
    private string purpleHappyTile = "happyPurple";
    private string redTile = "Red";
    private string redHappyTile = "happyRed";
    private string yellowTile = "Yellow";
    private string yellowHappyTile = "happyYellow";


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
        //DontDestroyOnLoad(gameObject);
        Debug.Log("Awake del Board");
        //GameManager.Instance.board = this;

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
                    if (tile.ToString().Contains(cyanHappyTile))
                    {
                        Debug.Log("CYAN en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.CYAN, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }else if (tile.ToString().Contains(greenHappyTile))
                    {
                        Debug.Log("GREEN en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.GREEN, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                    else if (tile.ToString().Contains(orangeHappyTile))
                    {
                        Debug.Log("ORANGE en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.ORANGE, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                    else if (tile.ToString().Contains(purpleHappyTile))
                    {
                        Debug.Log("PURPLE en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.PURPLE, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                    else if (tile.ToString().Contains(redHappyTile))
                    {
                        Debug.Log("RED en: " + col + "," + row);
                        HappyPiece happyPiece = Instantiate(happyPiecePrefab, position, Quaternion.identity);
                        happyPiece.Init(this, position, Color.RED, this.happyPieces.Count);
                        this.happyPieces.Add(happyPiece);
                    }
                    else if (tile.ToString().Contains(yellowHappyTile))
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

    void Start()
    {
        Debug.Log("Start del Board");

        //if (!GameManager.Instance.inGame) return;

        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        //this.happyPiecePrefab = (HappyPiece)Resources.Load("pPrefabs/HappyPiece", typeof(GameObject));
        this.happyPieces = new List<HappyPiece>();
        FindHappyPieces();

        for (int i = 0; i < this.tetrominoData.Length; i++)
        {
            this.tetrominoData[i].Init();
        }

        SpawnPiece();
    }

    public void SpawnPiece()
    {
         
        int random = Random.Range(0, this.tetrominoData.Length-1);
        TetrominoData data = this.tetrominoData[random];

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
        List<int> happyIds = new List<int>();
        foreach (HappyPiece happyPiece in happyPieces)
        {
            positionsToRemove.AddRange(GetLinePositions(happyPiece.position, happyPiece.color));
            if (positionsToRemove.Count >= 4)
            {
                happyIds.Add(happyPiece.id);
            }
        }

        // Remove Happy Pieces
        if (happyIds.Count > 0)
        {
            foreach (int id in happyIds) happyPieces.RemoveAll(happyPiece => happyPiece.id == id);
        }

        // Clear those cells
        foreach (Vector3Int position in positionsToRemove) this.tilemap.SetTile(position, null);
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
        while (/*!this.Bounds.Contains((Vector2Int)position) && */this.tilemap.HasTile(position) && AreSameColor(this.tilemap.GetTile<Tile>(position).ToString(), color)){
            positionsWithSameColor.Add(position);
            position.x += direction;
        }
        return positionsWithSameColor;
    }

    private List<Vector3Int> SameColorVerticalNeighbour(int direction, Vector3Int position, Color color)
    {
        List<Vector3Int> positionsWithSameColor = new List<Vector3Int>();
        position.y += direction;
        while (/*!this.Bounds.Contains((Vector2Int)position) &&*/ this.tilemap.HasTile(position) && AreSameColor(this.tilemap.GetTile<Tile>(position).ToString(), color))
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
                if (tileName.Contains(cyanTile)) return true;
                else return false;
            case Color.GREEN:
                if (tileName.Contains(greenTile)) return true;
                else return false;
            case Color.ORANGE:
                if (tileName.Contains(orangeTile)) return true;
                else return false;
            case Color.PURPLE:
                if (tileName.Contains(purpleTile)) return true;
                else return false;
            case Color.RED:
                if (tileName.Contains(redTile)) return true;
                else return false;
            case Color.YELLOW:
                if (tileName.Contains(yellowTile)) return true;
                else return false;
            default:
                return false;
        }
    }

}
