using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
  public GameObject objectToDeactivate;

  void OnMouseDown() {
      objectToDeactivate.SetActive(false);
  }
}

