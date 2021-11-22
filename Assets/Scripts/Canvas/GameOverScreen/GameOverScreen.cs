using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
  public Text winner;

  public void ShowGameOver()
  {
    gameObject.SetActive(true);
  }

  public void HideGameOver()
  {
    gameObject.SetActive(false);
  }

  public void SetWinner(TeamColor color)
  {
    switch (color)
    {
      case TeamColor.BLACK:
        winner.text = "Black Won";
        break;

      case TeamColor.WHITE:
        winner.text = "White Won";
        break;

      default:
        winner.text = "Draw";
        break;
    }
  }
}
