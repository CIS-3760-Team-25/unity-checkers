using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform target;
  public Vector3 offset;


  private void Start()
  {
    transform.position = target.position + offset;
    transform.LookAt(target);
  }

  public void Update()
  {
   // Any camera features i.e zoom in/out, look around       
  }


}
