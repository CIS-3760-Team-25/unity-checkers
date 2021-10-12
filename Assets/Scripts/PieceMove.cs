using UnityEngine;
using System.Collections;

public struct PieceMove
{
  public Vector3 start;
  public Vector3 delta;
  public float zCoord;

  public PieceMove(Vector3 startPos, Vector3 worldPos)
  {
    start = startPos;
    zCoord = worldPos.z;
    delta = new Vector3();
  }
}
