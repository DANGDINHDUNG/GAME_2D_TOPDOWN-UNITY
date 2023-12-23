using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitDoorManager : MonoBehaviour
{
    private bool enterAllowed;
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
            playerPosData.PlayerPosLoad();
        }
    }
}
