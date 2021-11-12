using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
  // Start is called before the first frame update
  void OnMouseEnter() 
  {
    GetComponent<Renderer>().material.color = Color.grey;
  }
    
}
