using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreedingMenu : MonoBehaviour
{
    // button stuff
    public Button button;
    public Text HPtext;
    public Text ATKtext;
    public Text DEFtext;
    public Text SPATKtext;
    public Text SPDEFtext;
    public Text SPEtext;
    public Text NAMEtext;
    public Text WORLDtext;


    private Item item;
    private BreedingScroll scrollList;



    //public Text display;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    public void Setup(Item currentItem, BreedingScroll currentScroll, bool selected)
    {
        item = currentItem;

        HPtext.text = item.HPtext.ToString();
        ATKtext.text = item.ATKtext.ToString();
        DEFtext.text = item.DEFtext.ToString();
        SPATKtext.text = item.SPATKtext.ToString();
        SPDEFtext.text = item.SPDEFtext.ToString();
        SPEtext.text = item.SPEtext.ToString();
        NAMEtext.text = item.NAMEtext;
        WORLDtext.text = item.WORLDtext;

        scrollList = currentScroll;

        /*if(selected)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.red;
            button.colors = colors;

        }*/
    }

    public void HandleClick()
    {
        scrollList.tryAddSedud(item);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
