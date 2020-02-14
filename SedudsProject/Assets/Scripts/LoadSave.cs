using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LoadSave
{
    public static List<Game> stuffToSave = new List<Game>();

    public static void Save()
    {
        stuffToSave.Add(Game.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saved.gd");
        bf.Serialize(file, LoadSave.stuffToSave);
        file.Close();
    }

    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/saved.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saved.gd", FileMode.Open);
            LoadSave.stuffToSave = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
    }
}

