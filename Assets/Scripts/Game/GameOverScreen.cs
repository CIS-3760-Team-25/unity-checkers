using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
  public Text winnerTxt;

  public void Setup(TeamColor winner)
  {
    gameObject.SetActive(true);

    switch (winner)
    {
      case TeamColor.BLACK:
        winnerTxt.text = "Black Won!";
        break;

      case TeamColor.WHITE:
        winnerTxt.text = "White Won!";
        break;

      default:
        winnerTxt.text = "Tie";
        break;
    }
  }

  public void MainMenuButton()
  {
    SceneManager.LoadScene("MenuScene");
  }

  public void RestartButton()
  {
    SceneManager.LoadScene("GameScene");
  }
}
