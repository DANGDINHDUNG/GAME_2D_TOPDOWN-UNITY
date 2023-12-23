using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeasonSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite _spring;
    [SerializeField] private Sprite _summer;
    [SerializeField] private Sprite _autumn;
    [SerializeField] private Sprite _winter;
    [SerializeField] string Season;
    [SerializeField] string currentSeason;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
        TimeManager.OnLoadedScene += OnLoadScene;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
        TimeManager.OnLoadedScene -= OnLoadScene;
    }

    private void UpdateDateTime(DateTime dateTime)
    {
        SpriteChanged(dateTime);
    }

    private void OnLoadScene(DateTime dateTime)
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            SpriteChanged(dateTime);
        }
    }

    void SpriteChanged(DateTime dateTime)
    {
        Season = dateTime.Season.ToString();
        if (Season == "Spring")
        {
            spriteRenderer.sprite = _spring;
        }
        else if (Season == "Winter")
        {
            spriteRenderer.sprite = _winter;

        }
        else if (Season == "Summer")
        {
            spriteRenderer.sprite = _summer;

        }
        else if (Season == "Autumn")
        {
            spriteRenderer.sprite = _autumn;
        }
    }
}
