using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgilityBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxAgilityPoint(float ap)
    {
        slider.maxValue = ap;
        slider.value = 0;
    }

    public void SetAgilityPoint(float ap)
    {
        slider.value = ap;
    }
}
