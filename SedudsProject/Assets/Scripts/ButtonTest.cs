using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// button functions
public class ButtonTest : MonoBehaviour
{
    public GameObject settings, controls, pause;
    public void MMbutton()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }

    public void SETbutton()
    {
        SceneManager.LoadScene(sceneName: "Settings");
    }

    public void NEWbutton()
    {
        SceneManager.LoadScene(sceneName: "New");
    }

    public void LOADbutton()
    {
        SceneManager.LoadScene(sceneName: "Load");
    }
    public void STARTbutton()
    {
        SceneManager.LoadScene(sceneName: "Start");
    }
    public void CONTbutton()
    {
        SceneManager.LoadScene(sceneName: "DomeEarth");

    }
    // changes panels in settings
    // pulls up controls
    public void OpenCont()
    {
        settings.SetActive(false);

        controls.SetActive(true);

    }
    public void CloseCont()
    {
        settings.SetActive(true);

        controls.SetActive(false);
    }
    public void OpenSettings()
    {
        settings.SetActive(true);

    }
    public void CloseSettings()
    {
        settings.SetActive(false);

    }
    public void MAPSbutton()
    {
        SceneManager.LoadScene(sceneName: "World1");

    }

    // closes pause menu
    public void leavePause()
    {
        pause.SetActive(false);
    }

}
