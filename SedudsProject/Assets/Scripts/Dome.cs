using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dome : MonoBehaviour
{
    public void ChangeScene(string scene) 
    {
        PlayerPrefs.SetInt("previousLevel", SceneManager.GetActiveScene().buildIndex);
        
        SceneManager.LoadScene(scene);
        Debug.Log("Change Scene");
    }
}