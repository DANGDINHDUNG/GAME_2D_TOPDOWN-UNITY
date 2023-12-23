using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        slider.gameObject.SetActive(false);
    }

    public void SetHealth(float health)
    {
        slider.gameObject.SetActive(true);
        slider.value = health;
    }
}
