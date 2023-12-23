using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ClockManager : MonoBehaviour
{
    public RectTransform ClockFace;
    public TextMeshProUGUI Date, Time, Week;
    string Season;

    public Sprite spring;
    public Sprite summer;
    public Sprite winter;
    public Sprite autumn;

    public Image seasonChange;

    public Image weatherSprite;
    public Sprite[] weatherSprites;

    public float startingRotation;

    public UnityEngine.Rendering.Universal.Light2D sunLight;
    public float nightIntensity;
    public float dayIntensity;
    public AnimationCurve dayNightCurve;

    public float currentIntensity;

    private void Awake()
    {
        startingRotation = ClockFace.localEulerAngles.z;
        currentIntensity = sunLight.intensity;
    }

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
        TimeManager.OnLoadedScene += OnLoadScene;
        WeatherManager.OnWeatherChanged += UpdateWeather;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
        TimeManager.OnLoadedScene -= OnLoadScene;
        WeatherManager.OnWeatherChanged -= UpdateWeather;
    }

    private void UpdateWeather(Weather weather, Queue<Weather> weathers)
    {
        string currentWeather = weather.ToString();
        if (currentWeather == "Sunny")
        {
            weatherSprite.sprite = weatherSprites[0];
        }
        else if (currentWeather == "Rainy")
        {
            weatherSprite.sprite = weatherSprites[1];
        }
        else if (currentWeather == "Snowy")
        {
            weatherSprite.sprite = weatherSprites[2];
        }
        else if (currentWeather == "Windy")
        {
            weatherSprite.sprite = weatherSprites[3];
        }
    }

    private void UpdateDateTime(DateTime dateTime)
    {
        Date.text = dateTime.DateToString();
        Time.text = dateTime.TimeToString();
        Season = dateTime.Season.ToString();
        if (Season == "Spring")
        {
            seasonChange.sprite = spring;
        }
        else if (Season == "Winter")
        {
            seasonChange.sprite = winter;

        }
        else if (Season == "Summer")
        {
            seasonChange.sprite = summer;

        }
        else if (Season == "Autumn")
        {
            seasonChange.sprite = autumn;

        }

        Week.text = $"WK: {dateTime.CurrentWeek.ToString()}";
        //weatherSprite.sprite = weatherSprites[(int)WeatherManager.currentWeek];

        float t = (float)dateTime.Hour / 24f;

        float newRotation = Mathf.Lerp(0, 180, t);  // Quay một nữa hình tròn
        ClockFace.localEulerAngles = new Vector3(0, 0, -newRotation + startingRotation);  // Khoảng cách di chuyển của một lần quay

        float dayNightT = dayNightCurve.Evaluate(t);


        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            sunLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, dayNightT);
            currentIntensity = sunLight.intensity;
        }
    }


    private void OnLoadScene(DateTime dateTime)
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            sunLight.intensity = currentIntensity;
        }
        else
        {
            sunLight.intensity = 1;
        }
    }
}
