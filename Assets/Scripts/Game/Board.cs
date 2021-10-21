using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
  /* Holds the position of the top left tile
   */
  [SerializeField]
  private Transform bottomLeftAnchor;
  private GameController controller;

  private Piece[,] layout;
  private Piece[] pieces;

  private const int BoardSize = 8;
  private const float BoardSquareSize = 2.0F;

  void Awake()
  {
    layout = new Piece[8, 8];
    pieces = (Piece[])FindObjectsOfType(typeof(Piece));

    foreach (Piece piece in pieces)
    {
      int pieceX = piece.startPosition.x;
      int pieceY = piece.startPosition.y;

      layout[pieceX, pieceY] = piece;

      piece.SetBoard(this);
    }
  }

  public void SetController(GameController gameController)
  {
    /* controller = gameController;
     */
  }

  public void SelectPiece(Piece piece)
  {
    /* Display indicators on valid destination squares
     */
  }

  public void DeselectPiece(Piece piece)
  {
    UpdatePiecePosition(piece);

    if (ProcessMove(piece))
    {
      AlignPieceInSquare(piece);
      FindValidDestinations(piece);

      if (IsGamePlayable())
      {
        // End turn
      }
      else
      {
        // End game
      }
    }
    else
    {
      piece.UndoMove();
    }
  }

  private bool ProcessMove(Piece piece)
  {
    int px0 = piece.previousPosition.x;
    int py0 = piece.previousPosition.y;
    int px1 = piece.currentPosition.x;
    int py1 = piece.currentPosition.y;
    int dx = Math.Abs(px1 - px0);
    int dy = Math.Abs(py1 - py0);

    // Check if piece in in board bounds
    if (px1 < 0 || py1 < 0 || px1 >= BoardSize || py1 >= BoardSize)
    {
      return false;
    }
    // Check that a piece isn't already in the square
    if (layout[px1, py1] != null)
    {
      return false;
    }

    // Check dx and dy (switch)
    // Check if piece was captured

    // Update model
    layout[px0, py0] = null;
    layout[px1, py1] = piece;

    return true;
  }

  private Piece WasPieceCaptured(Vector2Int previousPos, Vector2Int currentPos)
  {
    /* Check if previous and current position have a difference of 2
     * Difference of 2 implies a jump was made
     * If difference is 2, return the Piece that was captured
     * Otherwise return null
     */
    return null;
  }

  private void RemovePiece(Piece piece)
  {
    /* Set piece.isActive to false
     *
     */
  }

  private bool IsGamePlayable()
  {
    /* Make sure each team has pieces
     * Make sure each Piece in layout has valid destinations
     */
    return true;
  }

  private void UpdatePiecePosition(Piece piece)
  {
    Vector3 piecePosition = piece.transform.position;
    Vector2Int boardPosition = new Vector2Int();

    float dX = piecePosition.x - bottomLeftAnchor.position.x;
    float dZ = piecePosition.z - bottomLeftAnchor.position.z;

    boardPosition.x = (int)((dX) / BoardSquareSize);
    boardPosition.y = (int)((dZ) / BoardSquareSize);

    piece.previousPosition = piece.currentPosition;
    piece.currentPosition = boardPosition;
  }

  private void FindValidDestinations(Piece piece)
  {
    /* Iterate over the 8 squares surrounding Piece.currentPosition
     * Return list of valid move destinations
     */
  }

  private void AlignPieceInSquare(Piece piece)
  {
    int pX = 1 + (2 * piece.currentPosition.x);
    int pY = 1 + (2 * piece.currentPosition.y);

    piece.transform.position = new Vector3(pX, piece.transform.position.y, pY);
  }
}
