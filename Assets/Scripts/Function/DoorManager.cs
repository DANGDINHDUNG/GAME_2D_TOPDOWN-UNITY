using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public string sceneToLoad;

    SavePlayerPos playerPosData;
    private void Start()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("NPC"))
        {
            playerPosData.PlayerPosSave();
            SceneManager.LoadScene(sceneToLoad);
            playerPosData.PlayerReset();
        }
    }
}
