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
  private List<Piece> pieces;

  private const int BoardSize = 8;
  private const float BoardSquareSize = 2.0F;

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
    Vector3 piecePosition = piece.transform.position;
    Vector2Int boardPosition = new Vector2Int();

    float dX = piecePosition.x - bottomLeftAnchor.position.x;
    float dZ = piecePosition.z - bottomLeftAnchor.position.z;

    boardPosition.x = (int)((dX) / BoardSquareSize);
    boardPosition.y = (int)((dZ) / BoardSquareSize);

    piece.previousPosition = piece.currentPosition;
    piece.currentPosition = boardPosition;
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
    if (IsPositionOnBoard(left))
    {
      Piece leftSquarePiece = GetPieceAtPosition(left);

      if (leftSquarePiece == null)
      {
        piece.validDestinations.Add(left);
      }
      else if (leftSquarePiece.color != piece.color)
      {
        Vector2Int leftJump = left + new Vector2Int(-1, direction);

        if (IsPositionOnBoard(leftJump) && !GetPieceAtPosition(leftJump))
        {
          piece.validDestinations.Add(leftJump);
        }
      }
    }

    if (IsPositionOnBoard(right))
    {
      Piece rightSquarePiece = GetPieceAtPosition(right);

      if (rightSquarePiece == null)
      {
        piece.validDestinations.Add(right);
      }
      else if (rightSquarePiece.color != piece.color)
      {
        Vector2Int rightJump = right + new Vector2Int(1, direction);

        if (IsPositionOnBoard(rightJump) && !GetPieceAtPosition(rightJump))
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

  private void AlignPieceInSquare(Piece piece)
  {
    piece.transform.position = new Vector3(
      1 + (2 * piece.currentPosition.x),
      piece.transform.position.y,
      1 + (2 * piece.currentPosition.y)
    );
  }

  private bool IsPositionOnBoard(Vector2Int p)
  {
    return !(p.x < 0 || p.y < 0 || p.x >= BoardSize || p.y >= BoardSize);
  }
}
