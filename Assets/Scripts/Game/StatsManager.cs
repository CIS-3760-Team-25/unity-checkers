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

  public void StartGame(string gameData, System.Action<string> callback)
  {
    StartCoroutine(Post($"{API_URL}/games", gameData, callback));
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
}
