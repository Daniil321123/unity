using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    private string filePath;
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/save.gamedata";
        LoadGame();
    }


    public void SaveGame(Save save)
    {
        BinaryFormatter bf = new BinaryFormatter();
        //open file for write varibale 
        FileStream fs = new FileStream(filePath, FileMode.Create);
        //parse variable in file and saved 
        bf.Serialize(fs, save);
        //close file 
        fs.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        //open file for write varibale 
        FileStream fs = new FileStream(filePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        Tuning tuning = GetComponent<Tuning>();
        CarSpawnerTest carSpawn = GetComponent<CarSpawnerTest>();
        if (tuning)
        {
            tuning.LoadData(save);
        }
        if (carSpawn) 
        {
            carSpawn.LoadData(save);
        }
        //close file 
        fs.Close();
    }
}
[System.Serializable]
public class Save
{
    //save selected car
    public int currentCar { get; set; }
    //save selected car bumper 
    public int currentFrontBumper { get; set; }
    public int currentRearBumper { get; set; }
    public int currentSpoiler { get; set; }
    public int currentRoof { get; set; }
    public int currentExhaust { get; set; }

    //сохранение цвета 
    public float r { get; set; }
    public float g { get; set; }
    public float b { get; set; }

    //сохранение для цвета стекол
    public float rW { get; set; }
    public float gW { get; set; }
    public float bW { get; set; }
    public float aW { get; set; }
}

[System.Serializable]
public class PaintSave
{
    public float r { get; set; }
    public float g { get; set; }
    public float b { get; set; }
}
