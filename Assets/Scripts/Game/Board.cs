using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
  private Piece[,] layout;
  private List<Piece> pieces;
  private GameController controller;

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
    /* controller = gameController;
     */
  }

  public void SelectPiece(Piece piece)
  {
    piece.validDestinations.ForEach(
      (Vector2Int pos) =>
      {
        Debug.Log($" > {pos}");
      }
    );
  }

  public void DeselectPiece(Piece piece)
  {
    UpdatePiecePosition(piece);

    if (ProcessMove(piece))
    {
      BoardUtils.AlignObjectInSquare(piece.gameObject);
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
    piece.currentPosition = BoardUtils.FlattenVector(piece.transform.position);
  }

  private void FindValidMoves(Piece piece)
  {
    piece.validDestinations.Clear();

    int direction = piece.color == TeamColors.WHITE ? -1 : 1;

    // Left and right is from the black player's perspective
    Vector2Int left = new Vector2Int(
      piece.currentPosition.x - 1,
      piece.currentPosition.y + (1 * direction)
    );
    Vector2Int right = new Vector2Int(
      piece.currentPosition.x + 1,
      piece.currentPosition.y + (1 * direction)
    );

    // TODO: Refactor this once indicators are showing
    if (BoardUtils.IsOnBoard(left))
    {
      Piece leftSquarePiece = GetPieceAtPosition(left);

      if (leftSquarePiece == null)
      {
        piece.validDestinations.Add(left);
      }
      else if (leftSquarePiece.color != piece.color)
      {
        Vector2Int leftJump = left + new Vector2Int(-1, direction);

        if (BoardUtils.IsOnBoard(leftJump) && !GetPieceAtPosition(leftJump))
        {
          piece.validDestinations.Add(leftJump);
        }
      }
    }

    if (BoardUtils.IsOnBoard(right))
    {
      Piece rightSquarePiece = GetPieceAtPosition(right);

      if (rightSquarePiece == null)
      {
        piece.validDestinations.Add(right);
      }
      else if (rightSquarePiece.color != piece.color)
      {
        Vector2Int rightJump = right + new Vector2Int(1, direction);

        if (BoardUtils.IsOnBoard(rightJump) && !GetPieceAtPosition(rightJump))
        {
          piece.validDestinations.Add(rightJump);
        }
      }
    }
  }

  private void FindAllValidMoves()
  {
    pieces.ForEach((Piece p) => FindValidMoves(p));
  }

  private Piece GetPieceAtPosition(Vector2Int position)
  {
    return layout[position.x, position.y];
  }
}
