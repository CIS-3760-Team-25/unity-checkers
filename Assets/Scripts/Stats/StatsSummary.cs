using System;
using System.Runtime;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsSummary
{
  public int gamesPlayed;
  public int wins;
  public int losses;
  public int draws;
  public int incompletes;
  public int totalCaptures;
  public int totalTime;
  public double avgGameLength;
  public double avgCaptures;

  public string ToJson()
  {
    return JsonUtility.ToJson(this);
  }
}
