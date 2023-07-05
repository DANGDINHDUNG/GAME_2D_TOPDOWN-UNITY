using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    public GameObject GUI;
    public bool active;
    static public GUIController instance;

    public GameObject btnGameOver;
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);

        if (instance != null) { Destroy(this.gameObject); }
        else
        {
            DontDestroyOnLoad(gameObject);
            GUIController.instance = this;
            this.btnGameOver = GameObject.Find("btnGameOver");
            this.btnGameOver.SetActive(false);
            GUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene") { Destroy(this.gameObject); }
        if (Input.GetKeyDown(KeyCode.G))
        {

            // Nếu Menu chưa bật. bật menu lên và dừng game lại
            if (!active)
            {
                GUI.transform.gameObject.SetActive(true);
                active = true;
            }
            // Nếu Menu đã bật, tắt Menu đi và cho game chạy lại
            else
            {
                GUI.transform.gameObject.SetActive(false);
                active = false;
            }
        }

        
    }
}
