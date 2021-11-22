using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
  public void ShowMainMenu()
  {
    gameObject.SetActive(true);
  }

  public void HideMainMenu()
  {
    gameObject.SetActive(false);
  }

  public void OpenGameRules()
  {
    Application.OpenURL("https://checkers.fandom.com/wiki/Rules_of_Checkers");
  }
}
