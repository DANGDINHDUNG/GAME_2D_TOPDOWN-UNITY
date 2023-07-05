using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Rendering.Universal;

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

    private void Awake()
    {
        startingRotation = ClockFace.localEulerAngles.z;
        sunLight = GameObject.FindGameObjectWithTag("Light").GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
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

        sunLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, dayNightT);
    }
}
