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
    if (Input.GetKeyDown("9"))
    {
      activePlayer = TeamColor.BLACK;
      EndGame();
    }

    if (Input.GetKeyDown("0"))
    {
      activePlayer = TeamColor.WHITE;
      EndGame();
    }
  }

  public void SetGameRecord(GameRecord record)
  {
    gameRecord = record;
  }

  public void StartGame()
  {
    // Called from PlayerSelectScreen
    activePlayer = TeamColor.BLACK;

    board.InitializeBoard();
    board.ActivatePlayerPieces(activePlayer);
    board.EnablePieces();
  }

  public void EndGame()
  {
    gameOverScreen.ShowGameOver();
    gameOverScreen.SetWinner(activePlayer);

    GameOutcome gameOutcome = new GameOutcome(gameRecord.gameId);

    gameOutcome.SetPlayerOneCaptures(board.playerOneCaptures);
    gameOutcome.SetPlayerTwoCaptures(board.playerTwoCaptures);

    if (activePlayer == TeamColor.BLACK)
      gameOutcome.SetOutcome("player_one_win");
    else if (activePlayer == TeamColor.WHITE)
      gameOutcome.SetOutcome("player_two_win");
    else
      gameOutcome.SetOutcome("draw");

    Debug.Log($"Game Outcome: {gameOutcome.ToJson()}");

    statsManager.EndGame(gameOutcome.ToJson(), result =>
      {
        Debug.Log($"Game Outcome Recorded: {result}");
      }
    );

    board.DisablePieces();
  }

  public void StartTurn()
  {
  }

  public void EndTurn()
  {
    Debug.Log($"{activePlayer} turn ended");

    activePlayer = (TeamColor)((int)activePlayer * (-1));
    board.ActivatePlayerPieces(activePlayer);
    // Temporarily disable camera rotation
    // cameraController.Rotate();
  }
}
