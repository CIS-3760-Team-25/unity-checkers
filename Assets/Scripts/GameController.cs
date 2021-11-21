using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
  public Board board;
  public TeamColor activePlayer;

  [SerializeField]
  private GameOverScreen gameOverScreen;

  private CameraController cameraController;

  [SerializeField]
  private StatsManager statsManager;
  private GameRecord gameRecord;

  void Awake()
  {
    cameraController = GetComponent<CameraController>();
    activePlayer = TeamColor.BLACK;
  }

  void Start()
  {
    board.SetController(this);
  }

  void Update()
  {
    // Temporary triggers for game over
    if (Input.GetKeyDown("z"))
      EndGame(TeamColor.BLACK);
    if (Input.GetKeyDown("x"))
      EndGame(TeamColor.WHITE);
  }

  public void SetGameRecord(GameRecord record)
  {
    gameRecord = record;
  }

  public void StartGame()
  {
    // Called from PlayerSelect
    activePlayer = TeamColor.BLACK;
    board.EnablePieces(activePlayer);
  }

  public void EndGame()
  {
    gameOverScreen.Setup(activePlayer);

  }
  public void EndGame(TeamColor winner)
  {
    // This function should determine winner, not accept param
    gameOverScreen.Setup(winner);

    GameOutcome gameOutcome = new GameOutcome(gameRecord.gameId);
    gameOutcome.SetOutcome("draw");
    gameOutcome.SetPlayerOneCaptures(0);
    gameOutcome.SetPlayerTwoCaptures(1);

    statsManager.EndGame(gameOutcome.ToJson(), result =>
      {
        Debug.Log($"Game outcome recorded: {result}");
      }
    );
  }

  public void StartTurn()
  {
  }

  public void EndTurn()
  {
    Debug.Log($"{activePlayer} turn ended");

    activePlayer = (TeamColor)((int)activePlayer * (-1));
    board.EnablePieces(activePlayer);
    // Temporarily disable camera rotation
    // cameraController.Rotate();
  }
}
