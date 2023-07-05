using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        GameObject.DontDestroyOnLoad(this.gameObject);

        if (player.Length != 1) { Destroy(this.gameObject); }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;

        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (SceneManager.GetActiveScene().name == "MenuScene") { Destroy(this.gameObject); }
    //}

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene") { Destroy(this.gameObject); }
    }
}
