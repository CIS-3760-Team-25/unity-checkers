using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
  private Vector3 mOffset;
  private float mZCoord;

  void onMouseDown() {
    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

    // Store offset = gameobject world pos - mouse world pos
    mOffset = gameObject.transform.position - GetMouseWorldPos();
  }

  private Vector3 GetMouseWorldPos() {
    // pixel coordinates (x, y)
    Vector3 mousePoint = Input.mousePosition;

    // z coordinate of the game object on screen
    mousePoint.z = mZCoord + 20;

    return Camera.main.ScreenToWorldPoint(mousePoint);
  }

  void OnMouseDrag() {
    transform.position = GetMouseWorldPos() + mOffset;
  }
  
}


