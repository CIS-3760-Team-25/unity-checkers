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

  public Text statsEmail;
  public Text gamesPlayed;
  public Text wins;
  public Text losses;
  public Text draws;
  public Text incompletes;
  public Text captures;
  public Text timePlayed;

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

  public void HidePlayerStats()
  {
    gameObject.SetActive(false);
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

          Debug.Log($"Player Stats Retrieved: {response.ToJson()}");

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
    statsEmail.text = searchEmail.text;
    gamesPlayed.text = stats.gamesPlayed.ToString();
    wins.text = stats.wins.ToString();
    losses.text = stats.losses.ToString();
    draws.text = stats.draws.ToString();
    incompletes.text = stats.incompletes.ToString();
    captures.text = stats.totalCaptures.ToString();
    timePlayed.text = $"{stats.totalTime / 1000}s";
  }
}
