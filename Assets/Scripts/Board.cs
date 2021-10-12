using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
  /* 2D array storing all board pieces
   */
  public Piece[,] layout = new Piece[8,8];

  /* Holds the position of the top left tile
   */
  private static Transform topLeftAnchor;

  /* Used with topLeftAnchor to convert 3D positions to 2D grid positions
   */
  private static float boardSquareSize;

  private GameController controller;

  void Awake()
  {
    /* layout = new Piece[8,8];
     * Initialize the piece prefabs with the Piece MonoBehaviour
     * Move this to a factory class if design patterns get marks
     */
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
    /* ProcessMove()
     * If IsBoardValid() is true, controller.EndTurn
     * Otherwise revert move and let turn continue
     */
  }

  private void ProcessMove(Piece piece)
  {
    /* Compare Piece.currentPosition and previousPosition
     * If currentPosition is null, Piece has been moved off board (call ReverseMove())
     * Check if WasPieceCaptured()
     * board.AlignPieceInSquare(piece)
     */
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
    /* Check if Piece is white or black
     * Remove it from the associated player
     */
  }

  private void ReverseMove(Piece piece)
  {
    /* Use piece.previousPosition to
     */
  }

  private bool IsBoardValid()
  {
    /* Make sure each team has pieces
     * Make sure each Piece in layout has valid destinations
     */
     return false;
  }

  private List<Vector2Int> GenerateValidDestinations(Piece piece)
  {
    /* Iterate over the 8 squares surrounding Piece.currentPosition
     * Return list of valid move destinations
     */
     return new List<Vector2Int>();
  }

  private static Vector2Int FlattenVector(Vector3 vector3)
  {
    /* Convert a 3D vector from click position
     * to a 2D vector representing a board position
     * using topLeftAnchor and boardSquareSize
     * Return null if Vector3 is outside the board
     */
     return new Vector2Int();
  }

  private static void AlignPieceInSquare(Piece piece)
  {
    /* Move Piece's gameObject to the center of the square
     * Current square is stored in Piece.currentPosition
     */
  }
}
