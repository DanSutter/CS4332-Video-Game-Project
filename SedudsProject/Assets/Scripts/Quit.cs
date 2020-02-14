using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{

    public void OnButton()
    {
        Debug.Log("button pressed 2");
        Application.Quit();
    }
}
