using UnityEngine;

public static class BoardUtils
{
  public static int BoardSize = 8;
  public static float BoardSquareSize = 2.0F;
  private static Vector3 bottomLeftAnchor = new Vector3(0, 1.15F, 0);

  public static bool IsPositionOnBoard(Vector2Int p)
  {
    return p.x >= 0 && p.y >= 0 && p.x < BoardSize && p.y < BoardSize;
  }

  public static Vector2Int VectorToPosition(Vector3 vector)
  {
    Vector2Int boardPosition = new Vector2Int();

    vector -= bottomLeftAnchor;

    boardPosition.x = (int)(vector.x / BoardSquareSize);
    boardPosition.y = (int)(vector.z / BoardSquareSize);

    return boardPosition;
  }

  public static Vector3 PositionToVector(Vector2Int boardPosition)
  {
    Vector3 vector = new Vector3();

    vector += bottomLeftAnchor;

    vector.x = 1 + (boardPosition.x) * BoardSquareSize;
    vector.z = 1 + (boardPosition.y) * BoardSquareSize;

    return vector;
  }
}
