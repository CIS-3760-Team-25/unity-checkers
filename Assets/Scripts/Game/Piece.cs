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

  void OnMouseDown()
  {
    /* Create new Move
     * board.SelectPiece(this);
     */
  }

  void OnMouseDrag()
  {
    /* Update Move
     * Some draggable object code:
     * https://www.patreon.com/posts/unity-3d-drag-22917454
     * Piece shouldn't be draggable if not active
     */
  }

  void onMouseUp()
  {
    /* SetPosition(...) of the piece
     * board.DeselectPiece(this);
     */
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
}
