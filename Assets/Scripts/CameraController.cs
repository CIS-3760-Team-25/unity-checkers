using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public float rot_duration = 0.5f;
  public float rot_speed = 0.5F;
  Quaternion final_rot;

  bool isRotating = false;

  void Start()
  {
    final_rot = transform.rotation;
  }

  public void Rotate()
  {
    if (!isRotating)
      StartCoroutine("rotateOBJ");
  }

  IEnumerator rotateOBJ()
  {
    final_rot = final_rot * Quaternion.Euler(0, 0, 180);
    Quaternion startRot = transform.rotation;
    isRotating = true;
    float rot_elapsedTime = 0.0F;
    while (rot_elapsedTime < rot_duration)
    {
      transform.rotation = Quaternion.Slerp(startRot, final_rot, rot_elapsedTime / rot_duration * 2);
      rot_elapsedTime += Time.deltaTime;
      yield return null;
    }
    isRotating = false;
  }
}
