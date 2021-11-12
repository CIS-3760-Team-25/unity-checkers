using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
  public GameObject objectToDestroy;

  void OnMouseDown() {
      objectToDestroy.SetActive(false);
  }
}

