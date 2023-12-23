using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransition : MonoBehaviour
{
    public static MenuTransition instance;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);

        if (instance != null) { Destroy(this.gameObject); }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

        }
    }
}
