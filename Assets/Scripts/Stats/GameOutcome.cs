using System;
using System.Runtime;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameOutcome
{
  public string gameId;
  public string outcome;
  public int playerOneCaptures;
  public int playerTwoCaptures;

  public GameOutcome(string id)
  {
    gameId = id;
  }

  public void SetOutcome(string gameOutcome)
  {
    outcome = gameOutcome;
  }

  public void SetPlayerOneCaptures(int captures)
  {
    playerOneCaptures = captures;
  }

  public void SetPlayerTwoCaptures(int captures)
  {
    playerTwoCaptures = captures;
  }

  public string ToJson()
  {
    return JsonUtility.ToJson(this);
  }
}
