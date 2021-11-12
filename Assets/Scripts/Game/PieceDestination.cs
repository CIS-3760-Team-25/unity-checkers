using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDestination
{
  public Vector2Int position;
  public Piece capturedPiece;

  public PieceDestination(Vector2Int pos)
  {
    position = pos;
  }

  public PieceDestination(Vector2Int pos, Piece cap)
  {
    position = pos;
    capturedPiece = cap;
  }
}
