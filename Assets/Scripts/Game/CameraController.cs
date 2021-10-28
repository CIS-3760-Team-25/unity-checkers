using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rot_duration = 2f;
    public float rot_speed = 0.25F;
    Quaternion final_rot;
 
    bool isRotating = false;
 
    // Use this for initialization
    void Start()
    {
        final_rot = transform.rotation;
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
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") && !isRotating) StartCoroutine("rotateOBJ");
    }

    
}
