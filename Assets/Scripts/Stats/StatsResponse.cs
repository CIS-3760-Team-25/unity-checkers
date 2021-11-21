using System;
using System.Runtime;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsResponse
{
  public StatsSummary data;
  public string error;

  public string ToJson()
  {
    return JsonUtility.ToJson(this);
  }
}
