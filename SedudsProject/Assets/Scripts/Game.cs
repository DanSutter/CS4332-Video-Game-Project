using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// create savable game
public class Game
{
    public static Game current;
    public string gameName { get; set; }

    public Game()
    {

    }
}
