using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    void OnMouseEnter() 
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    void onMouseExit() {
        GetComponent<Renderer>().material.color = Color.blue;
    }
    
}
