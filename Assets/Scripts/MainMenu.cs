using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public int state;
    public bool isInMenuScene = true;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);

        if (instance != null) { Destroy(this.gameObject); }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;

        }
    }
}
