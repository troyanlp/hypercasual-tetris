using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyPiece : MonoBehaviour
{

    public Board board { get; private set; }
    public Vector3Int position { get; private set; }

    public void Init(Board board, Vector3Int position)
    {
        this.board = board;
        this.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
