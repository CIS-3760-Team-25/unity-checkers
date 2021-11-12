using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{

    public GameObject menuGameObject;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CreatePrefab();
        }
    }

    public void CreatePrefab()
    {
        Instantiate(menuGameObject);
    }
}
