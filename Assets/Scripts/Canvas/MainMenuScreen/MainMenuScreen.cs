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
}
