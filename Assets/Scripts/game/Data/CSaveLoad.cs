﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class CSaveLoad
{
    public static int equipped = 0;
    public static int money = 0;
    public static List<int> bought = new List<int>();
    public static List<int> unlocked = new List<int>();
    public static int bestScore = 0;

    public static float soundVolume = 1;
    public static float musicVolume = 0.2f;

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

        bf = new BinaryFormatter();
        file = File.Create(Application.persistentDataPath + "/unlocked.gd");
        bf.Serialize(file, CSaveLoad.unlocked);
        file.Close();

        bf = new BinaryFormatter();
        file = File.Create(Application.persistentDataPath + "/score.gd");
        bf.Serialize(file, CSaveLoad.bestScore);
        file.Close();

        bf = new BinaryFormatter();
        file = File.Create(Application.persistentDataPath + "/sound.gd");
        bf.Serialize(file, CSaveLoad.soundVolume);
        file.Close();

        bf = new BinaryFormatter();
        file = File.Create(Application.persistentDataPath + "/music.gd");
        bf.Serialize(file, CSaveLoad.musicVolume);
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

        if (File.Exists(Application.persistentDataPath + "/unlocked.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/unlocked.gd", FileMode.Open);
            CSaveLoad.unlocked = (List<int>)bf.Deserialize(file);
            file.Close();
        }

        if (File.Exists(Application.persistentDataPath + "/score.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/score.gd", FileMode.Open);
            CSaveLoad.bestScore = (int)bf.Deserialize(file);
            file.Close();
        }

        if (File.Exists(Application.persistentDataPath + "/sound.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/sound.gd", FileMode.Open);
            CSaveLoad.soundVolume = (float)bf.Deserialize(file);
            file.Close();
        }

        if (File.Exists(Application.persistentDataPath + "/music.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/music.gd", FileMode.Open);
            CSaveLoad.musicVolume = (float)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void eraseLoad()
    {
        equipped = 0;
        bought = new List<int>();
        money = 0;
        unlocked = new List<int>();
        bestScore = 0;
        musicVolume = 0.2f;
        soundVolume = 1;
        save();
    }

    //public static void setMusicVolume(float aVolume)
    //{
    //    musicVolume = aVolume;
    //}

    //public static void setSoundVolume(float aVolume)
    //{
    //    soundVolume = aVolume;
    //}

    public static void setBestScore(int aScore)
    {
        if (aScore > bestScore)
        {
            bestScore = aScore;
        }
    }

    public static bool isBought(int aIndex)
    {
        return bought.Contains(aIndex);
    }
}
