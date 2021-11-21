using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectScreen : MonoBehaviour
{
  [SerializeField]
  private StatsManager statsManager;

  [SerializeField]
  private GameController gameController;

  public InputField playerOneName;
  public InputField playerOneEmail;
  public InputField playerTwoName;
  public InputField playerTwoEmail;

  public Text playerOneNameError;
  public Text playerOneEmailError;
  public Text playerTwoNameError;
  public Text playerTwoEmailError;

  private bool isPlayerOneGuest = false;
  private bool isPlayerTwoGuest = false;

  void Start()
  {
    playerOneNameError.gameObject.SetActive(false);
    playerOneEmailError.gameObject.SetActive(false);
    playerTwoNameError.gameObject.SetActive(false);
    playerTwoEmailError.gameObject.SetActive(false);
  }

  public void ShowPlayerSelect()
  {
    gameObject.SetActive(true);
  }

  public void CreateGameRecord()
  {
    if (ArePlayersValid())
    {
      GameRecord gameRecord = new GameRecord();
      gameRecord.SetPlayerOne(playerOneName.text, playerOneEmail.text);
      gameRecord.SetPlayerTwo(playerTwoName.text, playerTwoEmail.text);

      gameController.SetGameRecord(gameRecord);
      gameController.StartGame();

      statsManager.StartGame(gameRecord.ToJson(), result =>
        {
          Debug.Log($"Game record created: {result}");
        }
      );

      gameObject.SetActive(false);
    }
  }

  public void TogglePlayerOneGuest(Toggle toggle)
  {
    isPlayerOneGuest = toggle.isOn;

    playerOneName.interactable = !isPlayerOneGuest;
    playerOneEmail.interactable = !isPlayerOneGuest;

    if (isPlayerOneGuest)
    {
      playerOneName.text = "Guest";
      playerOneEmail.text = "";

      playerOneNameError.gameObject.SetActive(false);
      playerOneEmailError.gameObject.SetActive(false);
    }

    Debug.Log($"Player One is guest? {isPlayerOneGuest}");
  }

  public void TogglePlayerTwoGuest(Toggle toggle)
  {
    isPlayerTwoGuest = toggle.isOn;

    playerTwoName.interactable = !isPlayerTwoGuest;
    playerTwoEmail.interactable = !isPlayerTwoGuest;

    if (isPlayerTwoGuest)
    {
      playerTwoName.text = "Guest";
      playerTwoEmail.text = "";

      playerTwoNameError.gameObject.SetActive(false);
      playerTwoEmailError.gameObject.SetActive(false);
    }

    Debug.Log($"Player Two is guest? {isPlayerTwoGuest}");
  }

  public void ClearFieldErrors(string field)
  {
    switch (field)
    {
      case "PlayerOneName":
        playerOneNameError.gameObject.SetActive(playerOneName.text.Length == 0);
        break;
      case "PlayerOneEmail":
        playerOneEmailError.gameObject.SetActive(playerOneEmail.text.Length == 0);
        break;
      case "PlayerTwoName":
        playerTwoNameError.gameObject.SetActive(playerTwoName.text.Length == 0);
        break;
      case "PlayerTwoEmail":
        playerTwoEmailError.gameObject.SetActive(playerTwoEmail.text.Length == 0);
        break;
    }
  }

  private bool ArePlayersValid()
  {
    if (!isPlayerOneGuest)
    {
      playerOneNameError.gameObject.SetActive(playerOneName.text.Length == 0);
      // TODO: Make sure email is valid email
      playerOneEmailError.gameObject.SetActive(playerOneEmail.text.Length == 0);
    }

    if (!isPlayerTwoGuest)
    {
      playerTwoNameError.gameObject.SetActive(playerTwoName.text.Length == 0);
      // TODO: Make sure email is valid email
      playerTwoEmailError.gameObject.SetActive(playerTwoEmail.text.Length == 0);
    }

    return ( // If any field has an error, not valid
      !playerOneNameError.gameObject.activeInHierarchy &&
      !playerOneEmailError.gameObject.activeInHierarchy &&
      !playerTwoNameError.gameObject.activeInHierarchy &&
      !playerTwoEmailError.gameObject.activeInHierarchy
    );
  }
}
