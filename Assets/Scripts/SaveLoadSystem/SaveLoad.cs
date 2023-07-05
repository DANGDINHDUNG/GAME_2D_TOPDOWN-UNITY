using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SaveLoad
{
    public static string SaveDirectory = "/SaveData/";
    public static string FileName = "SaveGame.sav";

    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;

    public static bool SaveGame(SaveData data)
    {
        OnSaveGame?.Invoke();
        string dir = Application.persistentDataPath + SaveDirectory;

        GUIUtility.systemCopyBuffer = dir;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(dir + FileName, json);

        Debug.Log("Saving game");
        return true;
    }

    public static SaveData LoadGame()
    {


        string fullPath = Application.persistentDataPath + SaveDirectory + FileName;

        SaveData tempData = new SaveData();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            tempData = JsonUtility.FromJson<SaveData>(json);

            OnLoadGame?.Invoke(tempData);

        }
        else
        {
            Debug.LogError("Save file does not exist!");
        }

        return tempData;
    }

    public static void DeleteSaveData()
    {
        string fullPath = Application.persistentDataPath + SaveDirectory + FileName;

        if (File.Exists(fullPath)) File.Delete(fullPath);
    }
}
