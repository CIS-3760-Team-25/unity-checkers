using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
  // public Player blackPlayer;
  // public Player whitePlayer;
  // public Player activePlayer;

 // public Board board;
  public GameOverScreen gameOverScreen;
  // private EventSystem menuEventSystem;
  // private EventSystem gameEventSystem;

  public void triggerGameOverScreen(int winner)
  {
    gameOverScreen.Setup(winner);
  }
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
  void Update()
  {
    //Temporarily triggers for game over
    if (Input.GetKeyDown("z")) triggerGameOverScreen(1);
    if (Input.GetKeyDown("x")) triggerGameOverScreen(-1);
  }

}
