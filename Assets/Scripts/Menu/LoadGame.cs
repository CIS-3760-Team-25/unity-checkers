using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
  void OnMouseDown() 
  {
    SceneManager.LoadScene("GameScene");
  }

}
