using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;

public class StatsManager : MonoBehaviour
{
  [SerializeField]
  private string API_URL;

  void Start()
  {
    Debug.Log($"API URL: {API_URL}");
  }

  public void StartGame(string gameRecord, System.Action<string> callback)
  {
    StartCoroutine(Post($"{API_URL}/games", gameRecord, callback));
  }

  public void EndGame(string gameOutcome, System.Action<string> callback)
  {
    StartCoroutine(Put($"{API_URL}/games", gameOutcome, callback));
  }

  public void GetSummary(string email, System.Action<string> callback)
  {
    StartCoroutine(Get($"{API_URL}/stats?email={email}", callback));
  }

  private IEnumerator Get(string url, System.Action<string> callback)
  {
    using (UnityWebRequest www = UnityWebRequest.Get(url))
    {
      yield return www.SendWebRequest();

      if (www.isNetworkError)
      {
        Debug.Log($"Network error: {www.error}");
      }
      else
      {
        if (www.isDone)
          callback(System.Text.Encoding.UTF8.GetString(www.downloadHandler.data));
        else
          Debug.Log($"Get request to {url} failed");
      }
    }
  }

  private IEnumerator Post(string url, string data, System.Action<string> callback)
  {
    using (UnityWebRequest www = UnityWebRequest.Post(url, data))
    {
      www.SetRequestHeader("content-type", "application/json");
      www.uploadHandler.contentType = "application/json";
      www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(data));
      yield return www.SendWebRequest();

      if (www.isNetworkError)
      {
        Debug.Log($"Network error: {www.error}");
      }
      else
      {
        if (www.isDone)
          callback(System.Text.Encoding.UTF8.GetString(www.downloadHandler.data));
        else
          Debug.Log($"Post request to {url} failed");
      }
    }
  }

  private IEnumerator Put(string url, string data, System.Action<string> callback)
  {
    using (UnityWebRequest www = UnityWebRequest.Put(url, data))
    {
      www.SetRequestHeader("content-type", "application/json");
      www.uploadHandler.contentType = "application/json";
      www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(data));
      yield return www.SendWebRequest();

      if (www.isNetworkError)
      {
        Debug.Log($"Network error: {www.error}");
      }
      else
      {
        if (www.isDone)
          callback(System.Text.Encoding.UTF8.GetString(www.downloadHandler.data));
        else
          Debug.Log($"Put request to {url} failed");
      }
    }
  }
}
