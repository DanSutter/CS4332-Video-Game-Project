using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeColor : MonoBehaviour
{
    public GameObject part;
    public GameObject current, next;

    public string MyColor;  //color of the button

    // used to change color of object to button color
    public void colorButton()
    {
        //one materal per color, instead of one material per sedud part
        //remember not to change SpaceSpecial's texture
        switch (MyColor)
        {
            //load material from resources folder and change material upon button press
            case "blue":
                part.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudBlue") as Material;
                break;
            case "green":
                part.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudGreen") as Material;
                break;
            case "red":
                part.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudRed") as Material;
                break;
            case "yellow":
                part.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudYellow") as Material;
                break;
        }
    }
    
    // switches between panels
    public void nextPanel()
    {
        next.SetActive(true);
        current.SetActive(false);
    }
}
