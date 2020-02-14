using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



[System.Serializable]
public class Item
{
    public float HPtext;
    public float ATKtext;
    public float DEFtext;
    public float SPATKtext;
    public float SPDEFtext;
    public float SPEtext;
    public string NAMEtext;
    public string WORLDtext;


    public string snout;
    public string ears;
    public string feet;
    public string horns;
    public string tail;
    public string wings;
    public string special;

}



public class BreedingScroll : MonoBehaviour
{
    public GameObject breedMenuPanel;

    public List<Item> itemList;
    //public List<Phys> PhysList;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;
    public List<Item> chosenList;

    public bool selected = false;

    // get sedud information
    public Sedud sedudInfo;

    // breed!
    public Button breedButton;

    // baby name
    public InputField InputField;
    public Text nameText;

    // Start is called before the first frame update
    void Start()
    {

        breedButton.onClick.AddListener(HandleBreedClick);

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {

        for (int j = 0; j < chosenList.Count; j++)
        {
            Debug.Log("chosen list: " + j + ": " + chosenList[j].NAMEtext);
        }

        RemoveButtons();
        AddButtons();
    }

    private void AddButtons()
    {
        GameObject[] sedudList;
        sedudList = GameObject.FindGameObjectsWithTag("creature");



        if (sedudList.Length == 0)
        {
            Debug.Log("No game objects are tagged with 'creature'");
        }


        for (int i = 0; i < sedudList.Length; i++)
        {
            Item item = itemList[i];
            sedudInfo = sedudList[i].GetComponent<Sedud>();

            itemList[i].HPtext = sedudInfo.HP;
            itemList[i].ATKtext = sedudInfo.ATK;
            itemList[i].DEFtext = sedudInfo.DEF;
            itemList[i].SPDEFtext = sedudInfo.SPDEF;
            itemList[i].SPATKtext = sedudInfo.SPATK;
            itemList[i].SPEtext = sedudInfo.SPE;
            itemList[i].NAMEtext = sedudInfo.name;
            itemList[i].WORLDtext = sedudInfo.World;


            itemList[i].snout = sedudInfo.Snout;
            Debug.Log("SNOUNT SCROLLL:" + itemList[i].snout);
            itemList[i].ears = sedudInfo.Ears;
            itemList[i].feet = sedudInfo.Feet;
            itemList[i].horns = sedudInfo.Horns;
            itemList[i].tail = sedudInfo.Tail;
            itemList[i].wings = sedudInfo.Wings;
            itemList[i].special = sedudInfo.Special;


        }
    


        for (int i = 0; i < sedudList.Length; i++)
        {
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel, false);

            BreedingMenu sampleButton = newButton.GetComponent<BreedingMenu>();

            if (chosenList.Contains(item))
            {
                selected = true;
            }

            sampleButton.Setup(item, this, selected);
            selected = false;

        }
    }
    private void RemoveButtons()
    {
        while(contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;

            buttonObjectPool.ReturnObject(toRemove);

        }
    }
    private void addSedud(Item itemToAdd, List<Item> sedList)
    {
        sedList.Add(itemToAdd);
    }
    private void removeSedud(Item itemToRemove, List<Item> sedList)
    {
        sedList.Remove(itemToRemove);
    }
    public void tryAddSedud(Item item)
    {


        if(chosenList.Contains(item))
        {
            Debug.Log("already here bub");

            removeSedud(item, chosenList);
            RefreshDisplay();
        }
        else
        {
            if(chosenList.Count < 2)
            {


                addSedud(item, chosenList);

                RefreshDisplay();
            }
        }

    }

    public List<Item> getChosen()
    {
        return chosenList;
    }

    public void HandleBreedClick()
    {
        


        nameText.text = InputField.text;
        Debug.Log("new name: " + nameText.text);

        Item itemA = chosenList[0];
        Item itemB = chosenList[1];

        GameObject newBreed;
        newBreed = GameObject.FindGameObjectWithTag("Nest");
        
        Breeding enterBreed = newBreed.GetComponent<Breeding>();

        enterBreed.sendParents(itemA, itemB, nameText.text);

        breedMenuPanel.SetActive(false);
    }
}
