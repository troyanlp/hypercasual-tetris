using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyPiece : MonoBehaviour
{
    public Board board { get; private set; }
    public Vector3Int position { get; private set; }

    public int id { get; private set; }
    public Color color { get; private set; }

    public void Awake()
    {
        Debug.Log("Awake Happy Piece");

    }

    public void Init(Board board, Vector3Int position, Color color, int id)
    {
        this.board = board;
        this.position = position;
        this.color = color;
        this.id = id;
    }

}
