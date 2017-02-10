using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class CSaveLoad
{
    public static int equipped = 0;
    public static int money = 0;
    public static List<int> bought = new List<int>();

    public static void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/equipped.gd");
        bf.Serialize(file, CSaveLoad.equipped);
        file.Close();

        bf = new BinaryFormatter();
        file = File.Create(Application.persistentDataPath + "/bought.gd");
        bf.Serialize(file, CSaveLoad.bought);
        file.Close();

        bf = new BinaryFormatter();
        file = File.Create(Application.persistentDataPath + "/money.gd");
        bf.Serialize(file, CSaveLoad.money);
        file.Close();
    }

    public static void load()
    {
        if (File.Exists(Application.persistentDataPath + "/equipped.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/equipped.gd", FileMode.Open);
            CSaveLoad.equipped = (int)bf.Deserialize(file);
            file.Close();
        }

        if (File.Exists(Application.persistentDataPath + "/bought.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/bought.gd", FileMode.Open);
            CSaveLoad.bought = (List<int>)bf.Deserialize(file);
            file.Close();
        }

        if (File.Exists(Application.persistentDataPath + "/money.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/money.gd", FileMode.Open);
            CSaveLoad.money = (int)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void eraseLoad()
    {
        equipped = 0;
        bought = new List<int>();
        money = 0;
        save();
    }
}
