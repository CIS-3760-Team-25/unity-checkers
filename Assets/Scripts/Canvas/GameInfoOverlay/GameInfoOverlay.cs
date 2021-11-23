using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInfoOverlay : MonoBehaviour
{
  public Text blackCaptures;
  public Text whiteCaptures;

  public GameObject blackTurnIndicator;
  public GameObject whiteTurnIndicator;

  public GameObject exitWarningPrompt;

  public void ShowGameOverlay()
  {
    gameObject.SetActive(true);
  }

  public void HideGameOverlay()
  {
    gameObject.SetActive(false);
  }

  public void ShowExitWarning()
  {
    exitWarningPrompt.SetActive(true);
  }

  public void HideExitWarning()
  {
    exitWarningPrompt.SetActive(false);
  }

  public void SetBlackCaptures(int count)
  {
    blackCaptures.text = $"{count}";
  }

  public void SetWhiteCaptures(int count)
  {
    whiteCaptures.text = $"{count}";
  }

  public void SwitchTurn()
  {
    blackTurnIndicator.SetActive(
      !blackTurnIndicator.gameObject.activeInHierarchy
    );

    whiteTurnIndicator.SetActive(
      !whiteTurnIndicator.gameObject.activeInHierarchy
    );
  }

  public void ResetOverlay()
  {
    blackTurnIndicator.SetActive(true);
    whiteTurnIndicator.SetActive(false);
    blackCaptures.text = "0";
    whiteCaptures.text = "0";
  }
}
