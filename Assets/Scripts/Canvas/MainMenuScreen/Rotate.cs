using UnityEngine;

class Rotate : MonoBehaviour
{
  void Update()
  {
    // Rotate 25 degrees per second around Y axis
    transform.Rotate(0, 25 * Time.deltaTime, 0);
  }
}
