using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour
{
  public TeamColors color;
  public Vector2Int startPosition;
  public Vector2Int currentPosition;
  public Vector2Int previousPosition;
  public List<Vector2Int> validDestinations;
  public bool isActive;

  private Board board;
  private PieceMove currentMove;

  void Awake()
  {
    this.currentPosition = startPosition;
  }

  void Start()
  {
    // Set Board as parent
    gameObject.transform.SetParent(board.transform);
  }

  void OnMouseDown()
  {
    currentMove = new PieceMove();
    currentMove.start = gameObject.transform.position;
    currentMove.zCoord = Camera.main.WorldToScreenPoint(currentMove.start).z;
    currentMove.offset = currentMove.start - GetCurrentPosition();

    Debug.Log($"Piece at ({currentPosition.x}, {currentPosition.y}) clicked");
  }

  void OnMouseDrag()
  {
    transform.position = GetCurrentPosition() + currentMove.offset;
  }

  void OnMouseUp()
  {
    transform.position = currentMove.start;

    Debug.Log($"Piece at ({currentPosition.x}, {currentPosition.y}) released");
    /* SetPosition(...) of the piece
     * board.DeselectPiece(this);
     */
  }

  public void SetBoard(Board gameBoard)
  {
    board = gameBoard;
  }

  public void SetPosition(Vector3 newPosition)
  {
    /* Use board.FlattenVector() to set position
     * previousPosition = currentPosition;
     * currentPosition = newPosition;
     */
  }

  public void UndoMove()
  {
    /* Apply the opposite of currentMove to the gameObject
     * board.AlignPieceInSquare(this)
     */
  }

  public void ResetPosition()
  {
    /* Apply the opposite of currentMove vector to gameObject
     */
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
