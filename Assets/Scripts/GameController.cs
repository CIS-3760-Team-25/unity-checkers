using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
  public Board board;
  public TeamColor activePlayer;

  [SerializeField]
  private GameOverScreen gameOverScreen;

  private CameraController cameraController;

  void Awake()
  {
    cameraController = GetComponent<CameraController>();
    activePlayer = TeamColor.BLACK;
  }

  void Start()
  {
    board.SetController(this);
    StartGame(); // Should be called after Play button is clicked
  }

  void Update()
  {
    // Temporary triggers for game over
    if (Input.GetKeyDown("z"))
      EndGame(TeamColor.BLACK);
    if (Input.GetKeyDown("x"))
      EndGame(TeamColor.WHITE);
  }

  public void StartGame()
  {
    activePlayer = TeamColor.BLACK;
    board.EnablePieces(activePlayer);
  }

  public void EndGame(TeamColor winner)
  {
    // This function should determine winner, not accept param
    gameOverScreen.Setup(winner);
  }

  public void StartTurn()
  {
  }

  public void EndTurn()
  {
    activePlayer = (TeamColor)((int)activePlayer * (-1));
    board.EnablePieces(activePlayer);
    cameraController.Rotate();
  }
}
