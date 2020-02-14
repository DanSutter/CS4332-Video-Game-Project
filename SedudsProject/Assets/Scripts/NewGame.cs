using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

// create new game
public class NewGame : MonoBehaviour
{
    public InputField Field;
    public Text TextBox;

    public void CopyText()
    {
        // name new game
        TextBox.text = Field.text;
        Debug.Log("GameName: " + TextBox.text);
        Game.current = new Game();

        Game.current.gameName = TextBox.text;

        LoadSave.Save();

        foreach (Game g in LoadSave.stuffToSave)
        {
            Debug.Log("saved: " + g.gameName);
        }
    }
}
