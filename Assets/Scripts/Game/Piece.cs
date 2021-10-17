using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
  public bool isActive;
  public TeamColors color;
  public Vector2Int startPosition;
  public Vector2Int currentPosition;
  public Vector2Int previousPosition;
  public List<Vector2Int> validDestinations;

  private Board board;
  private PieceMove currentMove;

  void Start()
  {
    // Set Board as parent
    gameObject.transform.SetParent(board.transform);
  }

  void OnMouseDown()
  {
    board.SelectPiece(this);

    currentMove = new PieceMove();
    currentMove.start = gameObject.transform.position;
    currentMove.zCoord = Camera.main.WorldToScreenPoint(currentMove.start).z;
    currentMove.offset = currentMove.start - GetCurrentPosition();

    Debug.Log($"{gameObject.name} clicked at {currentPosition}");
  }

  void OnMouseDrag()
  {
    transform.position = GetCurrentPosition() + currentMove.offset;
  }

  void OnMouseUp()
  {
    board.DeselectPiece(this);

    Debug.Log($"{gameObject.name} released at {currentPosition}");
  }

  public void SetBoard(Board gameBoard)
  {
    board = gameBoard;
  }

  public void UndoMove()
  {
    transform.position = currentMove.start;
    currentPosition = previousPosition;
  }

  private Vector3 GetCurrentPosition()
  {
    Vector3 currentPosition = new Vector3(
      Input.mousePosition.x,
      Input.mousePosition.y,
      currentMove.zCoord
    );

    return Camera.main.ScreenToWorldPoint(currentPosition);
  }
}
