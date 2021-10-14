using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHelp : MonoBehaviour
{
    void OnMouseEnter() 
    {
        SceneManager.LoadScene(2);
    }

}