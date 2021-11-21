using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScreen : MonoBehaviour
{
  [SerializeField]
  private StatsManager statsManager;

  [SerializeField]
  private GameObject playerSearchScreen;

  public InputField searchEmail;
  public Text searchError;

  public void ShowPlayerSearch()
  {
    playerSearchScreen.gameObject.SetActive(true);
  }

  public void HidePlayerSearch()
  {
    playerSearchScreen.gameObject.SetActive(false);
  }

  public void ShowPlayerStats()
  {
    gameObject.SetActive(true);
  }

  public void HideSearchErrors()
  {
    searchError.text = "";
  }

  public void SubmitSearch()
  {
    Debug.Log($"Searched for email: {searchEmail.text}");

    if (searchEmail.text.Length > 0)
    {
      statsManager.GetSummary(searchEmail.text, result =>
        {
          StatsResponse response = JsonUtility.FromJson<StatsResponse>(result);

          Debug.Log($"Player Stats Data: {response.data.ToJson()}");
          Debug.Log($"Player Stats Error: {response.error}");

          if (response.error.Length > 0)
          {
            searchError.text = response.error;
          }
          else
          {
            HidePlayerSearch();
            DisplayStatsSummary(response.data);
            ShowPlayerStats();
          }
        }
      );
    }
    else
    {
      searchError.text = "Please enter an email";
    }
  }

  private void DisplayStatsSummary(StatsSummary stats)
  {

  }
}
