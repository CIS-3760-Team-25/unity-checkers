using UnityEngine;

public static class BoardUtils
{
  private static int BoardSize = 8;
  private static float BoardSquareSize = 2.0F;
  private static Vector3 bottomLeftAnchor = new Vector3(0, 0, 0);

  public static void AlignObjectInSquare(GameObject gameOject)
  {
    Vector2Int currentPosition = FlattenVector(gameOject.transform.position);

    gameOject.transform.position = new Vector3(
      1 + (2 * currentPosition.x),
      gameOject.transform.position.y,
      1 + (2 * currentPosition.y)
    );
  }

  public static bool IsOnBoard(Vector2Int p)
  {
    return !(p.x < 0 || p.y < 0 || p.x >= BoardSize || p.y >= BoardSize);
  }

  public static Vector2Int FlattenVector(Vector3 position)
  {
    Vector2Int boardPosition = new Vector2Int();

    float dX = position.x - bottomLeftAnchor.x;
    float dZ = position.z - bottomLeftAnchor.z;

    boardPosition.x = (int)((dX) / BoardSquareSize);
    boardPosition.y = (int)((dZ) / BoardSquareSize);

    return boardPosition;
  }
}
