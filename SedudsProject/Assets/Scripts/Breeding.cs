using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breeding : MonoBehaviour
{
    //Var for nest
    bool nest = PlayerInteraction.nest;
    public string World;  //the world the player is in

    //Child var
    private GameObject parent;
    private GameObject Egg;  //this is a model of an egg. It only appears when Age=0.
    private GameObject egg; //instantiated clone egg
    private GameObject Child;    //final product
    private Transform ChildT;
    public GameObject template; //prefab - set in unity
    private GameObject model;

    // parents

    //Stats from Sedud's
    //P1
    float HP1 = 1f;
    float ATK1 = 1f;
    float DEF1 = 1f;
    float SPATK1 = 1f;
    float SPDEF1 = 1f;
    float SPE1 = 1f;
    //P2
    float HP2 = 1f;
    float ATK2 = 1f;
    float DEF2 = 1f;
    float SPATK2 = 1f;
    float SPDEF2 = 1f;
    float SPE2 = 1f;

    //Phys attributes from Seduds
    //P1
    string noses1;
    string ears1 ;
    string feet1;
    string horns1;
    string tail1;
    string wings1;
    
    //P2
    string noses2 ;
    string ears2 ;
    string feet2 ;
    string horns2 ;
    string tail2 ;
    string wings2 ;
    

    //Child Stats
    float HP = 1f;
    float ATK = 1f;
    float DEF = 1f;
    float SPATK = 1f;
    float SPDEF = 1f;
    float SPE = 1f;

    //Child Attributes
    string snout ;
    string ears ;
    string feet ;
    string horns ;
    string tail ;
    string wings ;
    string special;
    
    //Chance var
    int chance = 0;
    string P1, P2;

    //baby name
    string babyname;

    public void sendParents(Item parentA, Item parentB,string bn)
    {


        //baby name
        babyname = bn;
        
        //Stats from parents
        //P1
        HP1 = parentA.HPtext; //it says text, but it's not anymore
        ATK1 = parentA.ATKtext;
        DEF1 = parentA.DEFtext;
        SPATK1 = parentA.SPATKtext;
        SPDEF1 = parentA.SPDEFtext;
        SPE1 = parentA.SPEtext;
        //P2
        HP2 = parentB.HPtext;
        ATK2 = parentB.ATKtext;
        DEF2 = parentB.DEFtext;
        SPATK2 = parentB.SPATKtext;
        SPDEF2 = parentB.SPDEFtext;
        SPE2 = parentB.SPEtext;

        //P1
        noses1 = parentA.snout;
        ears1 = parentA.ears;
        feet1 = parentA.feet;
        horns1 = parentA.horns;
        tail1 = parentA.tail;
        wings1 = parentA.wings;
     
        //P2
        noses2 = parentB.snout;
        ears2 = parentB.ears;
        feet2 = parentB.feet;
        horns2 = parentB.horns;
        tail2= parentB.tail;
        wings2 = parentB.wings;
        


        //create baby stats
        HP = Random.Range(HP1, HP2);
        ATK = Random.Range(ATK1, ATK2);
        DEF = Random.Range(DEF1, DEF2);
        SPATK = Random.Range(SPATK1, SPATK2);
        SPDEF = Random.Range(SPDEF1, SPDEF2);
        SPE = Random.Range(SPE1, SPE2);

        //create baby attributes
        snout = selectChance(noses1, noses2, 0, 2);
        ears = selectChance(ears1, ears2, 3, 11);
        feet = selectChance(feet1, feet2, 12, 18);
        horns = selectChance(horns1, horns2, 19, 25);
        tail = selectChance(tail1, tail2, 26, 32);
        wings = selectChance(wings1, wings2, 33, 37);
        special = "Special"+World;

        //Call Spawner
        Spawn();
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        //Stats from Sedud's
        //P1
        HP1 = GameObject.Find("AllParts").GetComponent<Sedud>().HP;
        ATK1 = GameObject.Find("AllParts").GetComponent<Sedud>().ATK;
        DEF1 = GameObject.Find("AllParts").GetComponent<Sedud>().DEF;
        SPATK1 = GameObject.Find("AllParts").GetComponent<Sedud>().SPATK;
        SPDEF1 = GameObject.Find("AllParts").GetComponent<Sedud>().SPDEF;
        SPE1 = GameObject.Find("AllParts").GetComponent<Sedud>().SPE;
        //P2
        HP2 = GameObject.Find("AllParts").GetComponent<Sedud>().HP;
        ATK2 = GameObject.Find("AllParts").GetComponent<Sedud>().ATK;
        DEF2 = GameObject.Find("AllParts").GetComponent<Sedud>().DEF;
        SPATK2 = GameObject.Find("AllParts").GetComponent<Sedud>().SPATK;
        SPDEF2 = GameObject.Find("AllParts").GetComponent<Sedud>().SPDEF;
        SPE2 = GameObject.Find("AllParts").GetComponent<Sedud>().SPE;
        */
    }

    // Update is called once per frame
    void Update()
    {

        /*if (nest == true)
        {
            //Get Stats
            HP = Random.Range(HP1, HP2);
            ATK = Random.Range(ATK1, ATK2);
            DEF = Random.Range(DEF1, DEF2);
            SPATK = Random.Range(SPATK1, SPATK2);
            SPDEF = Random.Range(SPDEF1, SPDEF2);
            SPE = Random.Range(SPE1, SPE2);


            //Get attributes
            snout = selectChance(noses1, noses2,0,2);
            ears = selectChance(ears1, ears2,3,11);
            feet = selectChance(feet1, feet2,12,18);
            horns = selectChance(horns1, horns2,19,25);
            tail = selectChance(tail1, tail2,26,32);
            wings = selectChance(wings1, wings2,33,37);
            special = World;

            //Call Spawner
            Spawn();
        }
        */
    }

    string selectChance(string P1,string P2, float start, float end)
    {
        chance = Random.Range(0, 10);
        //Calculating chances of phys attributes
        if (chance <= 4)
        {
            //Returns P1's phys attribute
            return P1;
        }
        if (chance <= 8 && chance > 4)
        {
            //Returns P2's phys attribute
            return P2;
        }
        else 
        {
            float whichAtt = Random.Range(start, end);
            //switch to return attributes
            switch (whichAtt)
            {
                //noses
                case 0:
                    return "ShortSnout";
                case 1:
                    return "AvgSnout";
                case 2:
                    return "LongSnout";
                //ears
                case 3:
                    return "EarsBat";
                case 4:
                    return "EarsCat";
                case 5:
                    return "EarsSheep";
                case 6:
                    return "EarsDog";
                case 7:
                    return "EarsBunnyUp";
                case 8:
                    return "EarsBunnyDown";
                case 9:
                    return "EarsHuman";
                case 10:
                    return "EarsWolf";
                case 11:
                    return "EarsLong";
                //feet
                case 12:
                    return "FeetNone";
                case 13:
                    return "FeetNone";
                case 14:
                    return "FeetNone";
                case 15:
                    return "FeetBird";
                case 16:
                    return "FeetPaw";
                case 17:
                    return "FeetHoof";
                case 18:
                    return "FeetWebbed";
                //horns
                case 19:
                    return "HornsNone";
                case 20:
                    return "HornsAntennae";
                case 21:
                    return "HornsBack";
                case 22:
                    return "HornsForward";
                case 23:
                    return "HornsPointy";
                case 24:
                    return "HornsRound";
                case 25:
                    return "HornsTall";
                //tail
                case 26:
                    return "TailCat";
                case 27:
                    return "TailDog";
                case 28:
                    return "TailFox";
                case 29:
                    return "TailWolf";
                case 30:
                    return "TailDragon";
                case 31:
                    return "TailFish";
                case 32:
                    return "TailDevil";
                //wings
                case 33:
                    return "WingsNone";
                case 34:
                    return "WingsButterfly";
                case 35:
                    return "WingsBat";
                case 36:
                    return "WingsSwirl";
                case 37:
                    return "WingsAngel";
                default:
                    return("None");
            }
        }
    }

    

    void Spawn()
    {
        parent = new GameObject(babyname);
        parent.tag = "CreatureRoot";

        //egg, child
        Egg = (GameObject)Resources.Load("Egg"); //set egg model
        egg = Instantiate(Egg, parent.transform);   //make egg

        //sedud, child
        Child = Instantiate(template, parent.transform);  //create new Sedud with all parts

        //fill out sedud information in gameobject's variables
        Child.GetComponent<Sedud>().HP = HP;
        Child.GetComponent<Sedud>().ATK = ATK;
        Child.GetComponent<Sedud>().DEF = DEF;
        Child.GetComponent<Sedud>().SPATK = SPATK;
        Child.GetComponent<Sedud>().SPDEF = SPDEF;
        Child.GetComponent<Sedud>().SPE = SPE;

        Child.GetComponent<Sedud>().Name = name;
        Child.GetComponent<Sedud>().Snout = snout;
        Child.GetComponent<Sedud>().Feet = feet;
        Child.GetComponent<Sedud>().Tail = tail;
        Child.GetComponent<Sedud>().Ears = ears;
        Child.GetComponent<Sedud>().Horns = horns;
        Child.GetComponent<Sedud>().Wings = wings;
        Child.GetComponent<Sedud>().Special = special;

        //hide all parts except ones chosen
        for (int i = 0; i < Child.transform.childCount; i++)
        {
            model = Child.transform.GetChild(i).gameObject;
            //Debug.Log("model name: " + model.name);
            //Debug.Log("child snout: " + child.Snout);
            if (model.name != snout && model.name != feet && model.name != tail &&
                model.name != ears && model.name != horns && model.name != wings &&
                model.name != special)  //if not one of chosen parts
            {
                model.SetActive(false); //turn off part
            }
            else if (model.name != "SpecialSpace") //if chosen part, and chosen part is not SpecialSpace, give it a random color
            {
                int color = Random.Range(1,5);
                switch(color)
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
