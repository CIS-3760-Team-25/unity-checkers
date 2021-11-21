using System;
using System.Runtime;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameRecord
{
  public string gameId;
  public PlayerRecord playerOne;
  public PlayerRecord playerTwo;

  [Serializable]
  public class PlayerRecord
  {
    public string name;
    public string email;

    public PlayerRecord(string playerName, string playerEmail)
    {
      name = playerName;
      email = playerEmail;
    }
  }

  public GameRecord()
  {
    gameId = Guid.NewGuid().ToString();
  }

  public void setPlayerOne(string name, string email)
  {
    playerOne = new PlayerRecord(name, email);
  }

  public void setPlayerTwo(string name, string email)
  {
    playerTwo = new PlayerRecord(name, email);
  }

  public string ToJson()
  {
    return JsonUtility.ToJson(this);
  }
}
