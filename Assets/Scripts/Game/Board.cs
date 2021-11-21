using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
  public Mesh kingMesh; 
  private Piece[,] layout;
  private List<Piece> pieces;
  private GameController controller;

  [SerializeField]
  private GameObject whiteIndicator;

  [SerializeField]
  private GameObject blackIndicator;

  private List<GameObject> activeIndicators;

  private bool mustCapture;

  enum MoveOutcome
  {
    VALID, INVALID, CAPTURE
  }

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

  public void EnablePieces(TeamColor color)
  {
    mustCapture = false;

    pieces.ForEach(
      piece =>
      {
        piece.isActive = piece.color == color;
        // Move must involve a capture if any piece can capture
        if (piece.isActive && piece.HasCaptureMoves())
          mustCapture = true;
      }
    );

    Debug.Log(mustCapture ? $"{color} must capture" : $"{color} free to move");
  }

  public void SelectPiece(Piece piece)
  {
    if (piece.isActive)
      DisplayIndicators(piece);
  }

  public void DeselectPiece(Piece piece)
  {
    DestroyIndicators();
    UpdatePiecePosition(piece);

    if (piece.isActive)
    {
      switch (ProcessMove(piece))
      {
        case MoveOutcome.VALID:
          AlignPieceInSquare(piece);

          if (piece.HasReachedOppositeEndOfBoard() && !piece.isKing)
            piece.PromoteToKing();
          AlignPieceInSquare(piece);
          FindAllValidMoves();

          if (IsGamePlayable())
          {
            controller.EndTurn();
          }
          else
          {
            // End game
            controller.EndGame();
          }

          break;

        case MoveOutcome.INVALID:
          piece.UndoMove();
          break;

        case MoveOutcome.CAPTURE:

          AlignPieceInSquare(piece);
          FindAllValidMoves();

          if (!IsGamePlayable())
          {
            controller.EndGame();
          }
     
          ClearAllMoves();

          if (piece.HasReachedOppositeEndOfBoard() && !piece.isKing)
            piece.PromoteToKing();

          FindValidMoves(piece);

          // If not more captures available, end turn
          if (!piece.HasCaptureMoves())
          {
            FindAllValidMoves();
            controller.EndTurn();
          }
          break;
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
        piece.moveDestinations = new List<PieceDestination>();
        piece.captureDestinations = new List<PieceDestination>();
        piece.kingMesh = kingMesh;
        // Add pieces to board model
        layout[piece.startPosition.x, piece.startPosition.y] = piece;
        // Connect piece to board
        piece.SetBoard(this);
      }
    );

    FindAllValidMoves();
  }

  private MoveOutcome ProcessMove(Piece piece)
  {
    if (mustCapture)
    {
      // Check if move to current position is involved a capture
      PieceDestination destination = piece.captureDestinations.Find(
        dest => dest.position == piece.currentPosition
      );
      // If destination isn't a capture, move is invalid
      if (destination == null)
        return MoveOutcome.INVALID;

      RemovePiece(destination.capturedPiece);

      layout[piece.previousPosition.x, piece.previousPosition.y] = null;
      layout[piece.currentPosition.x, piece.currentPosition.y] = piece;

      return MoveOutcome.CAPTURE;
    }
    else
    {
      bool isValidDestination = piece.moveDestinations.Exists(
        dest => dest.position == piece.currentPosition
      );

      if (!isValidDestination)
        return MoveOutcome.INVALID;

      layout[piece.previousPosition.x, piece.previousPosition.y] = null;
      layout[piece.currentPosition.x, piece.currentPosition.y] = piece;

      return MoveOutcome.VALID;
    }
  }

  private void RemovePiece(Piece piece)
  {
    layout[piece.currentPosition.x, piece.currentPosition.y] = null;
    piece.gameObject.SetActive(false);
    pieces.Remove(piece);

    Debug.Log($"Removed piece at {piece.currentPosition}");
  }

  private bool IsGamePlayable()
  {
    int moves = 0;
    pieces.ForEach(piece =>
    {
    if (piece.color != controller.activePlayer && (piece.moveDestinations.Count > 0 || piece.captureDestinations.Count > 0))
        moves++;
    });
    return moves > 0 ? true : false;
  }

  private void UpdatePiecePosition(Piece piece)
  {
    piece.previousPosition = piece.currentPosition;
    piece.currentPosition = BoardUtils.VectorToPosition(piece.transform.position);
  }

  private void FindAllValidMoves()
  {
    pieces.ForEach(FindValidMoves);
  }

  private void FindValidMoves(Piece piece)
  {
    piece.moveDestinations.Clear();
    piece.captureDestinations.Clear();

    GetMovesInDirection(piece, new Vector2Int(-1, (int)piece.color));
    GetMovesInDirection(piece, new Vector2Int(1, (int)piece.color));
    if (piece.isKing)
    {
      GetMovesInDirection(piece, new Vector2Int(-1, -(int)piece.color));
      GetMovesInDirection(piece, new Vector2Int(1, -(int)piece.color));
    }
  }

  private void GetMovesInDirection(Piece piece, Vector2Int direction, bool jumpsOnly = false)
  {
    Vector2Int move = piece.currentPosition + direction;

    if (BoardUtils.IsPositionOnBoard(move))
    {
      Piece pieceInSquare = GetPieceAtPosition(move);
      // If square is empty it's a valid move, otherwise check for jump
      if (pieceInSquare == null)
      {
        if (!jumpsOnly)
          piece.moveDestinations.Add(new PieceDestination(move));
      }
      else if (pieceInSquare.color != piece.color)
      {
        Vector2Int jump = move + direction;
        // Check that jump position is on the board and empty
        if (BoardUtils.IsPositionOnBoard(jump) && !GetPieceAtPosition(jump))
        {
          piece.captureDestinations.Add(new PieceDestination(jump, pieceInSquare));
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
    // Lambda for displaying indicators
    Action<PieceDestination> displayIndicators = (destination) =>
    {
      Vector3 position = BoardUtils.PositionToVector(destination.position);

      if (piece.color == TeamColor.BLACK)
        activeIndicators.Add(
          Instantiate(blackIndicator, position, Quaternion.identity)
        );
      else
        activeIndicators.Add(
          Instantiate(whiteIndicator, position, Quaternion.identity)
        );
    };

    if (mustCapture)
    {
      if (piece.HasCaptureMoves())
        piece.captureDestinations.ForEach(displayIndicators);
    }
    else
    {
      piece.moveDestinations.ForEach(displayIndicators);
    }
  }

  private void ClearAllMoves()
  {
    pieces.ForEach(piece =>
      {
        piece.moveDestinations.Clear();
        piece.captureDestinations.Clear();
      }
    );
  }

  private void DestroyIndicators()
  {
    activeIndicators.ForEach(indicator => Destroy(indicator));
  }
}
