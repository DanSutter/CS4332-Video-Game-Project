using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DO NOT USE IN GAME. WAS COMBINED WITH BREEDING SCRIPT. ONLY USE FOR TESTING PURPOSES.
//attached to a SedudSpawner empty GameObject

public class SedudSpawner : MonoBehaviour
{
    public GameObject template; //prefab. will be AllParts v2 model
    public GameObject Child;    //final product
    private Transform ChildT;
    private GameObject model;
    private GameObject parent;

    private GameObject Egg;  //this is a model of an egg. It only appears when Age=0.
    private GameObject egg; //instantiated clone egg

    //custom sedud for testing;
    string name = "Lil Guy";
    string snout = "ShortSnout";
    string feet = "FeetPaw";
    string tail = "TailWolf";
    string ears = "EarsCat";
    string horns = "HornsAntennae";
    string wings = "WingsAngel";
    string special = "SpecialSpace";

    void Start()
    {
        //new empty gameobject to be parent. will hold the Sedud class information
        parent = new GameObject("Sedud");

        //egg, child
        Egg = (GameObject)Resources.Load("Egg"); //set egg model
        egg = Instantiate(Egg,parent.transform);   //make egg
        
        //sedud, child
        Create();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Create()
    {
        Child = Instantiate(template,parent.transform);  //create new Sedud with all parts

        //fill out sedud information in gameobject's variables
        Child.GetComponent<Sedud>().Name = name;
        Child.GetComponent<Sedud>().Snout = snout;
        Child.GetComponent<Sedud>().Feet = feet;
        Child.GetComponent<Sedud>().Tail = tail;
        Child.GetComponent<Sedud>().Ears = ears;
        Child.GetComponent<Sedud>().Horns = horns;
        Child.GetComponent<Sedud>().Wings = wings;
        Child.GetComponent<Sedud>().Special = special;

        //hide all parts except ones chosen
        for (int i=0;i< Child.transform.childCount;i++)
        {
            model = Child.transform.GetChild(i).gameObject;
            //Debug.Log("model name: "+model.name);
            //Debug.Log("child snout: "+child.Snout);
            if (model.name!= snout && model.name!= feet && model.name!= tail && 
                model.name!= ears && model.name!= horns && model.name!= wings && 
                model.name!= special)  //if not one of chosen parts
            {
                model.SetActive(false); //turn off part
            }
            else if(model.name!="SpecialSpace") //if chosen part, and chosen part is not SpecialSpace, give it a random color
            {
                int color = Random.Range(1, 5);
                switch (color)
                {
                    case 1: //blue
                        model.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudBlue") as Material;
                        break;
                    case 2: //green
                        model.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudGreen") as Material;
                        break;
                    case 3: //red
                        model.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudRed") as Material;
                        break;
                    case 4: //yellow
                        model.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudYellow") as Material;
                        break;
                }
            }
        }
    }
}
