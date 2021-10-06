# checkers



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

  void StartGame()
  {

  }

  void EndGame()
  {

  }

  void StartTurn()
  {

  }

  void EndTurn()
  {

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
   * Used to convert 3D positions to 2D grid positions
   */
  private Transform topLeftAnchor;

  private GameController controller;

  void Awake()
  {
    layout = new Piece[8,8];
    /* Initialize the piece prefabs with the Piece MonoBehaviour
     * Move this to a factory class if design patterns get marks
     */
  }

  public void SetController(GameController)
  {
    /*
     */
  }

  public void SelectPiece(Piece)
  {
    /* Display indicators on valid destination squares
     */
  }

  public void DeselectPiece(Piece)
  {
    /* If layout.ValidateBoard() is valid, end turn
     * Otherwise revert move and let turn continue
     */
  }

  private bool Validate()
  {
    /* Make sure each team has pieces
     * Make sure each Piece in layout has valid destinations
     */
  }

  private Vector2[] GenerateValidDestinations(Piece)
  {
    /* Iterate over the 8 squares surrounding Piece.position
     * Return list of valid move destinations
     */
  }

  private Vector2Int FlattenVector(Vector3)
  {
    /* Convert a 3D vector from click position
     * to a 2D vector representing a board position
     */
  }
}
```



---



```c#
class Player
{
  List<Piece> activePieces;

  void AddPiece(Piece)
  {

  }

  void RemovePiece(Piece)
  {

  }
}
```



---



```c#
class Piece : MonoBehaviour
{
  private struct Move
  {
    private Vector3 delta;
    private Vector3 start;
    private float zCoord;

    private Move(Vector3, Vector3)
    {
      /* Set start and zCoord
       */
    }
  }

  public Vector2Int position;
  public Vector2Int validDestinations;
  public TeamColors color;

  private Board board;
  private Move move;

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
    /* SetPosition(...)
     * board.DeselectPiece(this);
     */
  }

  public void SetPosition(Vector3)
  {
    /* Use board.FlattenVector() to set position
     */
  }

  public void ResetPosition()
  {
    /* Do the opposite of move vector
     */
  }
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

