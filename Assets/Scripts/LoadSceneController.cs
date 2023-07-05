using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneController : MonoBehaviour
{
    private void Awake()
    {
        if (MainMenu.instance.state == 0)
        {
            this.gameObject.SetActive(false);
        }
        else if (MainMenu.instance.state == 1)
        {
            Time.timeScale = 0;
            this.gameObject.SetActive(true);
            MainMenu.instance.state = 0;
        }
    }
}
