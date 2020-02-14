using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    //Clicking variables
    Ray ray;
    RaycastHit hit;

    public static bool nest = false;

    //Calling codes
    bool Pet = true;

    //Audio
    AudioSource audio;

    //fruit count
    int fruit = 0;
    public Text fruitCount;

    // nestui
    public GameObject nestUI;

    //popui
    public GameObject popUI;
    public GameObject sudud;
    private Sedud first;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fruit
        if (Input.GetMouseButtonDown(0)) //Left button
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            //Fruit get
            if(Physics.Raycast(ray, out hit) && hit.collider.tag== "Fruit")
            {
                FruitGrow fruits = hit.collider.gameObject.GetComponent<FruitGrow>();
                fruits.picked = true;
                print("Tried to pick " + Input.mousePosition);

                fruit++;
                //Add signal to UI to add to Players Fruit Count
                fruitCount.text = "Fruit: " + fruit.ToString();
                Debug.Log("fruitcount: " + fruit);
            }

            //Creature pet
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "creature")
            {
                sudud = hit.collider.gameObject;
                Sedud creature = hit.collider.gameObject.GetComponent<Sedud>();
                if (creature != null)
                {
                    creature.isPet = true;
                }
                print("Pet Creature " + Input.mousePosition);
            }

            //Nest
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Nest")
            {
                print("Nest UI " + Input.mousePosition);
                nest = true;
                //Call UI
                if (nestUI.activeSelf)
                {
                    nestUI.SetActive(false);

                }
                else
                {
                    nestUI.SetActive(true);

                }
            }
        }

        if (Input.GetMouseButtonDown(1)) //Right button
        {
            //Creature
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "creature")
            {
                print("Creature UI " + Input.mousePosition);

                sudud = hit.collider.gameObject;
                popUI = sudud.transform.Find("popUpV2").gameObject;

                //Call UI
                if (popUI.activeSelf)
                {
                    popUI.SetActive(false);

                }
                else
                {
                    popUI.SetActive(true);


                    SedudList list = hit.collider.gameObject.GetComponent<SedudList>();
                    if (list != null)
                    {
                        list.test(sudud);
                    }
                    Debug.Log("*********************back here* ");
                }
                
            }
        }

        if(Input.GetKey(KeyCode.E))
        {
            print("Whistle for creatures");
            //Audio
            audio = GetComponent<AudioSource>();
            audio.Play(0);
            //sedud following
            SedudList whistling=new SedudList();
            whistling.changeWhistleAll();
        }

    }

    public void petButton()
    {
        Debug.Log("===sed: " + sudud.name);
    }
}
