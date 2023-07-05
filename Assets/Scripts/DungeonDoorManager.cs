using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDoorManager : MonoBehaviour
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
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            playerPosData.PlayerReset();
        }
    }
}
