using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    // sedud
    public GameObject GOfirst;
    private Sedud first;
    // name text
    public InputField InputField;
    public Text nameText;

    public GameObject APanel, BPanel, ASedud, BSedud;

    public void getName()
    {
        nameText.text = InputField.text;
        first.Name = nameText.text;
        first.name = nameText.text;
        first.transform.parent.gameObject.name = nameText.text;
    }
    private void Awake()
    {
        first = GOfirst.GetComponent<Sedud>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = InputField.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenA()
    {
        BPanel.SetActive(false);

        APanel.SetActive(true);

        BSedud.SetActive(false);

        ASedud.SetActive(true);

    }
    public void OpenB()
    {
        APanel.SetActive(false);

        BPanel.SetActive(true);

        ASedud.SetActive(false);

        BSedud.SetActive(true);
    }
}
