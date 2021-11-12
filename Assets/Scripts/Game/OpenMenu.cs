using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{

  public GameObject objectToActivate;
    
  // Update is called once per frame
  void Update()
  {
    if(Input.GetKeyDown(KeyCode.Escape))
    {
      objectToActivate.SetActive(true);
    }
  }

}
