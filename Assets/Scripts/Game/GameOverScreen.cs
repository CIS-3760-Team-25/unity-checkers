using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
  public Text winnerTxt;
  public void Setup(int winner)
  {
    gameObject.SetActive(true);

    if (winner == 1)
    {
      winnerTxt.text = "Black Won!";
    }
    else if (winner == -1)
    {
      winnerTxt.text = "White Won!";
    }
    else
    {
      winnerTxt.text = "Tie";
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
 