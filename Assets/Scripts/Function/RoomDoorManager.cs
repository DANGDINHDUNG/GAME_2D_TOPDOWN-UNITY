using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoorManager : MonoBehaviour
{
    public string sceneToLoad;

    SavePlayerPos playerPosData;
    private void Start()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPosData.PlayerPosSave();
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
