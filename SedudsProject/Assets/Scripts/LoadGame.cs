using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    Text savedGames;
    void Start()
    {
        savedGames = gameObject.GetComponent<Text>();
        foreach (Game g in LoadSave.stuffToSave)
        {
            Debug.Log("saved: " + g.gameName);
            savedGames.text = g.gameName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
