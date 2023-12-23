using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeasonTileMap : MonoBehaviour
{
    [SerializeField] private Grid _spring;
    [SerializeField] private Grid _summer;
    [SerializeField] private Grid _autumn;
    [SerializeField] private Grid _winter;
    [SerializeField] string Season;

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

        // Deactivate all palettes
        _spring.gameObject.SetActive(false);
        _summer.gameObject.SetActive(false);
        _autumn.gameObject.SetActive(false);
        _winter.gameObject.SetActive(false);

        // Activate the current season's palette
        switch (Season)
        {
            case "Spring":
                _spring.gameObject.SetActive(true);
                break;
            case "Summer":
                _summer.gameObject.SetActive(true);
                break;
            case "Autumn":
                _autumn.gameObject.SetActive(true);
                break;
            case "Winter":
                _winter.gameObject.SetActive(true);
                break;
        }
    }
}
