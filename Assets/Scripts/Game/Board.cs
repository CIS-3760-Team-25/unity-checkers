using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
  private Piece[,] layout;
  private List<Piece> pieces;
  private GameController controller;

  [SerializeField]
  private GameObject whiteIndicator;

  [SerializeField]
  private GameObject blackIndicator;

  private List<GameObject> activeIndicators;

  void Awake()
  {
    InitializeBoard();
  }

  void OnValidate()
  {
    InitializeBoard();
  }

  public void SetController(GameController gameController)
  {
    controller = gameController;
  }

  public void SelectPiece(Piece piece)
  {
    DisplayIndicators(piece);
  }

  public void DeselectPiece(Piece piece)
  {
    DestroyIndicators();
    UpdatePiecePosition(piece);

    if (ProcessMove(piece))
    {
      AlignPieceInSquare(piece);
      FindAllValidMoves();

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

  private void InitializeBoard()
  {
    layout = new Piece[8, 8];
    // Find all piece prefabs
    pieces = new List<Piece>(
      (Piece[])FindObjectsOfType(typeof(Piece))
    );

    activeIndicators = new List<GameObject>();

    pieces.ForEach((Piece piece) =>
      {
        piece.currentPosition = piece.startPosition;
        piece.previousPosition = piece.startPosition;
        // Add pieces to board model
        layout[piece.startPosition.x, piece.startPosition.y] = piece;
        // Connect piece to board
        piece.SetBoard(this);
      }
    );

    FindAllValidMoves();
  }

  private bool ProcessMove(Piece piece)
  {
    if (!piece.validDestinations.Contains(piece.currentPosition))
    {
      return false;
    }

    layout[piece.previousPosition.x, piece.previousPosition.y] = null;
    layout[piece.currentPosition.x, piece.currentPosition.y] = piece;

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
    piece.previousPosition = piece.currentPosition;
    piece.currentPosition = BoardUtils.VectorToPosition(piece.transform.position);
  }

  private void FindAllValidMoves()
  {
    pieces.ForEach(
      (Piece piece) =>
      {
        piece.validDestinations.Clear();

        /* Direction indicates which way the piece is moving in the layout
         * Black pieces moving forward is an increase in y value (dir = 1)
         * White pieces moving forward is a decrease in y value (dir = -1)
         * Note that "y" refers to Vector2Int.y rn, not gameObject.transform.y
         */
        int direction = piece.color == TeamColors.WHITE ? -1 : 1;

        // Check for moves/jumps in the left (-1) and right (1) direction
        GetMovesInDirection(piece, new Vector2Int(-1, direction));
        GetMovesInDirection(piece, new Vector2Int(1, direction));
      }
    );
  }

  private void GetMovesInDirection(Piece piece, Vector2Int direction)
  {
    Vector2Int move = piece.currentPosition + direction;

    if (BoardUtils.IsPositionOnBoard(move))
    {
      Piece pieceInSquare = GetPieceAtPosition(move);

      // If square is empty it's a valid move, otherwise check for jump
      if (pieceInSquare == null)
      {
        piece.validDestinations.Add(move);
      }
      else if (pieceInSquare.color != piece.color)
      {
        Vector2Int jump = move + direction;
        // Check that jump position is on the board and empty
        if (BoardUtils.IsPositionOnBoard(jump) && !GetPieceAtPosition(jump))
        {
          piece.validDestinations.Add(jump);
        }
      }
    }
  }

  private Piece GetPieceAtPosition(Vector2Int position)
  {
    return layout[position.x, position.y];
  }

  private void AlignPieceInSquare(Piece piece)
  {
    piece.transform.position = new Vector3(
      1 + (BoardUtils.BoardSquareSize * piece.currentPosition.x),
      piece.transform.position.y,
      1 + (BoardUtils.BoardSquareSize * piece.currentPosition.y)
    );
  }

  private void DisplayIndicators(Piece piece)
  {
    piece.validDestinations.ForEach(
      (Vector2Int piecePosition) =>
      {
        Vector3 position = BoardUtils.PositionToVector(piecePosition);

        if (piece.color == TeamColors.BLACK)
          activeIndicators.Add(
            Instantiate(blackIndicator, position, Quaternion.identity)
          );
        else
          activeIndicators.Add(
            Instantiate(whiteIndicator, position, Quaternion.identity)
          );
      }
    );
  }

  private void DestroyIndicators()
  {
    activeIndicators.ForEach(
      (GameObject indicator) => Destroy(indicator)
    );
  }
}
