using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LampManager : MonoBehaviour
{    
    public UnityEngine.Rendering.Universal.Light2D sunLight;

    private void Awake()
    {
        sunLight = GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
        TimeManager.OnDateTimeChanged += OnLoadScene;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
        TimeManager.OnDateTimeChanged -= OnLoadScene;
    }

    private void UpdateDateTime(DateTime dateTime)
    {
        LightChanged(dateTime);
    }

    private void OnLoadScene(DateTime dateTime)
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            LightChanged(dateTime);
        }
    }


    void LightChanged(DateTime dateTime)
    {
        if (dateTime.IsNight())
        {
            sunLight.intensity = 1;
        }
        else
        {
            sunLight.intensity = 0;
        }
    }
}
