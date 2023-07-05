using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    public static SaveData data;
    [SerializeField] private static GameObject loadSceneController;

    private void Awake()
    {
        loadSceneController = GameObject.Find("LoadSceneController");
        data = new SaveData();
        SaveLoad.OnLoadGame += LoadData;
    }

    public void DeleteData()
    {
        SaveLoad.DeleteSaveData();
    }

    public static void SaveData()
    {
        var saveData = data;

        SaveLoad.SaveGame(saveData);
    }

    public static void LoadData(SaveData _data)
    {
        data = _data;
    }

    public static void TryLoadData()
    {
        Time.timeScale = 1;
        loadSceneController.SetActive(false);
        SaveLoad.LoadGame();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
        MainMenu.instance.state = 0;
        MainMenu.instance.isInMenuScene = false;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
        MainMenu.instance.state = 1;
        MainMenu.instance.isInMenuScene = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
        MainMenu.instance.isInMenuScene = true;

    }
}
