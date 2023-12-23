using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeatherManager : MonoBehaviour
{
    [Header("=== Weather Management ===")]
    [SerializeField] private int tickBetweenWeather = 1;
    [SerializeField] private int weatherQueueSize = 4;
    private int currentWeatherTick = 0;
    [SerializeField] private Weather currentWeather = Weather.Sunny;
    public Weather CurrentWeather => currentWeather;
    private Queue<Weather> weatherQueue;

    [Header("=== Weather VFX ===")]
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] ParticleSystem snowParticles;
    [SerializeField] ParticleSystem windParticles;

    [Header("=== Debug Options ===")]
    public bool forceRain = false;

    public static Action<Weather, Queue<Weather>> OnWeatherChanged;

    private void Start()
    {
        rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        windParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        FillWeatherQueue();
        //ChangeWeather();
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
        currentWeatherTick++;

        if (currentWeatherTick >= tickBetweenWeather)
        {
            currentWeatherTick = 0;
            ChangeWeather();
        }
    }

    private void ChangeWeather()
    {
        currentWeather = weatherQueue.Dequeue();
        weatherQueue.Enqueue(GetRandomWeather());

        OnWeatherChanged?.Invoke(currentWeather, weatherQueue);

        switch (currentWeather)
        {
            case Weather.Sunny:
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                windParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case Weather.Rainy:
                snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                rainParticles.Play();
                windParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case Weather.Snowy:
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                snowParticles.Play();
                windParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case Weather.Windy:
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                windParticles.Play();
                break;
            default: break;
        }
    }

    private void FillWeatherQueue()
    {
        weatherQueue = new Queue<Weather>();

        for (int i = 0; i < weatherQueueSize; i++)
        {
            Weather tempWeather = GetRandomWeather();
            weatherQueue.Enqueue(tempWeather);
            //Debug.Log($"Weather is {tempWeather} at index {i}");
        }
    }

    private Weather GetRandomWeather()
    {
        int randomWeather = 0;

        randomWeather = UnityEngine.Random.Range(0, (int)Weather.WEATHER_MAX + 1);

        return (Weather)randomWeather;
    }
}

[System.Serializable]
public enum Weather
{
    Sunny = 0,
    Windy = 1,
    Rainy = 2,
    Snowy = 3,
    WEATHER_MAX = Snowy
}
