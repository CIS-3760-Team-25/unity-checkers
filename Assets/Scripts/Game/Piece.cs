using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
  [SerializeField]
  private Mesh defaultMesh;

  [SerializeField]
  private Mesh kingMesh;

  public bool isEnabled;
  public bool isActive;
  public bool isKing;
  public TeamColor color;
  public Vector2Int startPosition;
  public Vector2Int currentPosition;
  public Vector2Int previousPosition;
  public List<PieceDestination> moveDestinations;
  public List<PieceDestination> captureDestinations;

  private Board board;
  private PieceMove currentMove;
  private float heightToAddOnPickup = 1;

  void OnMouseDown()
  {
    if (isEnabled)
    {
      board.SelectPiece(this);

      currentMove = new PieceMove();
      currentMove.start = gameObject.transform.position;
      currentMove.zCoord = Camera.main.WorldToScreenPoint(currentMove.start).z;
      currentMove.offset = currentMove.start - GetCurrentPosition();

      Debug.Log($"{gameObject.name} clicked at {currentPosition}");
    }
  }

  void OnMouseDrag()
  {
    if (isEnabled)
      transform.position = GetCurrentPosition() + currentMove.offset + new Vector3(0,heightToAddOnPickup,0);
  }

  void OnMouseUp()
  {
    if (isEnabled)
    {
      if (board.DeselectPiece(this))
        transform.position -= new Vector3(0, heightToAddOnPickup, 0);
      
      Debug.Log($"{gameObject.name} released at {currentPosition}");
    }
  }

  public void SetBoard(Board gameBoard)
  {
    board = gameBoard;
    gameObject.transform.SetParent(board.transform);
  }

  public void ResetMesh()
  {
    this.isKing = false;
    this.gameObject.GetComponent<MeshFilter>().mesh = defaultMesh;
  }

  public void PromoteToKing()
  {
    this.isKing = true;
    this.gameObject.GetComponent<MeshFilter>().mesh = kingMesh;

    Debug.Log($"{gameObject.name} promoted at {currentPosition}");
  }

  public bool HasValidMoves()
  {
    return (moveDestinations.Count + captureDestinations.Count) != 0;
  }

  public bool HasCaptureMoves()
  {
    return captureDestinations.Count != 0;
  }

  public bool HasReachedOppositeEndOfBoard()
  {
    int rowToReach = this.color == TeamColor.BLACK ? 7 : 0;
    return this.currentPosition.y == rowToReach;
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
