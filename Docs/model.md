# checkers

The `Board` model is defined as a 2D array of `Piece` objects (each is an instance of GameObject). Each `Piece` position is defined as a 2D vector (`Vector2Int`). Positions are defined relative to the bottom left corner of the board as show below. The orange dot represents the position of `Board.bottomLeftAnchor` (used to convert 3D positions to array positions)

<p align="center" style="transform: rotate(90)">
  <img src="https://user-images.githubusercontent.com/45947696/141135530-0e23a252-af14-437c-9cd4-ab10a4251a7d.png" width="60%" >
</p>
<br>

```c#
/* Controls the flow of the app
 */
class GameController : MonoBehaviour
{
  public Player blackPlayer;
  public Player whitePlayer;
  public Player activePlayer;

  public Board board;

  private EventSystem menuEventSystem;
  private EventSystem gameEventSystem;

  void Awake()
  {
  }

  public void StartGame()
  {
  }

  public void EndGame()
  {
  }

  public void StartTurn()
  {
  }

  public void EndTurn()
  {
  }

  public void RemovePiece(Piece)
  {
    /* Check if Piece is white or black
     * Remove it from the associated player
     */
  }
}
```

---

```c#
class Board : MonoBehaviour
{
  /* 2D array storing all board pieces
   */
  private Piece[,] layout;

  /* Holds the position of the top left tile
   */
  private static Transform BottomLeftAnchor;

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

  public void SelectPiece(Piece)
  {
    /* Display indicators on valid destination squares
     */
  }

  public void DeselectPiece(Piece)
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
  }

  private void ReverseMove(Piece)
  {
    /* Use piece.previousPosition to
     */
  }

  private bool IsGamePlayable()
  {
    /* Make sure each team has pieces
     * Make sure each Piece in layout has valid destinations
     */
  }

  private void GenerateValidDestinations(Piece)
  {
    /* Iterate over the 8 squares surrounding Piece.currentPosition
     * Set piece.validDestinations
     */
  }
}
```

---

```c#
class Player
{
  private List<Piece> activePieces;

  public void AddPiece(Piece)
  {
    /* Add Piece to activePieces
     */
  }

  public void RemovePiece(Piece)
  {
    /* Remove Piece from activePieces
     */
  }

  public boolean HasPieces()
  {

  }

  public boolean HasMoves()
  {

  }
}
```

---

```c#
class Piece : MonoBehaviour
{
  public TeamColors color;
  public Vector2Int currentPosition;
  public Vector2Int previousPosition;
  public Vector2Int validDestinations;

  private Board board;
  private PieceMove currentMove;

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
```

---

```csharp
private struct PieceMove
{
  private Vector3 start;
  private Vector3 delta;
  private float zCoord;
}
```

---

```c#
class GameCamera
{
  void SwitchPerspective()
  {

  }
}
```

---

```c#
public enum TeamColors
{
  BLACK,
  WHITE
}
```

---

_This functionality can be moved out of Board eventually_

```c#
public class BoardUtils
{
  public static int BoardSize;
  public static float BoardSquareSize;
  public static Transform topLeftAnchor;

  public Vector2Int VectorToBoardPosition(Vector3 vector3)
  {
  }

  // Used for aligning pieces and indicators
  private void AlignObjectInSquare(Piece piece)
  {
  }
}
```
