using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SedudActions : MonoBehaviour
{
    Button petButt;
    Button feedButt;
    Button hitButt;
    Button mainButt;

    private PlayerInteraction plInt;

    public GameObject GOfirst;
    private Sedud first;
    private void Awake()
    {
       // first = GOfirst.GetComponent<Sedud>();
    }
    void Start()
    {
        //petButt.onClick.AddListener(PETbutton);

    }

    public void PETbutton()
    {
        //plInt.petButton();
    }

    public void FEEDbutton()
    {
        //first.isFed = true;
    }

    public void HITbutton()
    {
       // first.isHit = true;
    }
}
