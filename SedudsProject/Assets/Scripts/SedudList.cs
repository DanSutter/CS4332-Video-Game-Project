using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SedudList : MonoBehaviour
{
    
    public GameObject popUI;
    public GameObject sudud1;

    private Sedud first;

    public Text HPtext;
    public Text ATKtext;
    public Text DEFtext;
    public Text SPATKtext;
    public Text SPDEFtext;
    public Text SPEtext;
    public Text NAMEtext;

    // buttons
    public Button petButt;
    public Button feedButt;
    public Button hitButt;
    public Button mainButt;

    //the list of all seduds
    public GameObject[] sedudList;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        //just for buttons. if the variables exist, add listener
        if(petButt)
            petButt.onClick.AddListener(PETbutton);
        if(feedButt)
            feedButt.onClick.AddListener(FEEDbutton);
        if(hitButt)
            hitButt.onClick.AddListener(HITbutton);
        if(mainButt)
            mainButt.onClick.AddListener(MAINbutton);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void test(GameObject sudud2)
    {
        Debug.Log("***********sedudList****************** " + sudud2.name);
        first = sudud2.GetComponentInChildren<Sedud>();


        /* // test values
        first.HP = 4;
        first.ATK = 5;
        first.DEF = 6;
        first.SPATK = 7;
        first.SPDEF = 8;
        first.SPE = 9;
        first.name = "Lil dude";
        */
        HPtext.text = "HP : " + first.HP;
        ATKtext.text = "ATK : " + first.ATK;
        DEFtext.text = "DEF : " + first.DEF;
        SPATKtext.text = "SPATK : " + first.SPATK;
        SPDEFtext.text = "SPDEF : " + first.SPDEF;
        SPEtext.text = "SPE : " + first.SPE;
        NAMEtext.text = first.name;

    }

    public void PETbutton()
    {
        Debug.Log("*************pet**************** " + first.name);
        first.isPet = true;
        popUI.SetActive(false);
    }
    public void FEEDbutton()
    {
        Debug.Log("*************feed**************** " + first.name);
        first.isFed = true;
        popUI.SetActive(false);
    }
    public void HITbutton()
    {
        Debug.Log("*************hit**************** " + first.name);
        first.isHit = true;
        popUI.SetActive(false);
    }
    public void MAINbutton()
    {
        fillList();

        for (int i = 0; i < sedudList.Length; i++)
        {
            if(sedudList[i].GetComponentInChildren<Sedud>().isMain == true)
            {
                Debug.Log("*************prev main**************** " + first.name);
                first.isMain = false;
            }
        }

        Debug.Log("*************main**************** " + first.name);
        first.isMain = true;
        popUI.SetActive(false);
    }

    public void fillList() //fill the list with all existing seduds
    {
        sedudList = GameObject.FindGameObjectsWithTag("CreatureRoot");

        if (sedudList.Length == 0)
        {
            Debug.Log("No game objects are tagged with 'CreatureRoot'");
        }
    }
    public void clearList()
    {
        sedudList = null;
    }
    public void changeWhistleAll() //currently used to change the whistle variable on all seduds. can be adapted for other variables
    {
        fillList();

        for(int i = 0; i < sedudList.Length; i++)   //for all seduds
        {
            sedudList[i].GetComponentInChildren<Sedud>().whistle = !sedudList[i].GetComponentInChildren<Sedud>().whistle;   //flip the whistle bool
        }
    }
    public void setAllMoved()   //set all seduds' moved=true so they can properly integrate into new dome
    {
        Debug.Log("Number of seduds: "+sedudList.Length);
        for (int i = 0; i < sedudList.Length; i++)   //for all seduds
        {
            sedudList[i].GetComponentInChildren<Sedud>().moved = true;
        }
    }
}
