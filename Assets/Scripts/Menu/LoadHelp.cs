using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHelp : MonoBehaviour
{
  void OnMouseDown() {
    SceneManager.LoadScene("HelpScene");
  }

}