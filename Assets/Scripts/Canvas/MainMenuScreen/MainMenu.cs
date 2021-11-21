using UnityEngine;

public class MainMenu : MonoBehaviour
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
