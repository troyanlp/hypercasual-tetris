using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominoData;
    public Vector3Int spawnPosition;

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for(int i = 0; i < this.tetrominoData.Length; i++)
        {
            this.tetrominoData[i].Init();
        }
    }

    void Start()
    {
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        int random = Random.Range(0, this.tetrominoData.Length-1);
        TetrominoData data = this.tetrominoData[random];

        this.activePiece.Init(this, this.spawnPosition, data);
        SetPieceData(this.activePiece);
    }

    public void SetPieceData(Piece piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
