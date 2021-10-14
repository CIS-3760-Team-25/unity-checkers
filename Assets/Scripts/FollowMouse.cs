using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0)) {
                Vector3 temp = Input.mousePosition;
                temp.z = 18f;
                this.transform.position = Camera.main.ScreenToWorldPoint(temp);
        }      
 
    }

}
