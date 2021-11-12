using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
  void onMouseDrag() 
  {
    Vector3 temp = Input.mousePosition;
    temp.z = 18f;
    this.transform.position = Camera.main.ScreenToWorldPoint(temp);   
  }
  
}
