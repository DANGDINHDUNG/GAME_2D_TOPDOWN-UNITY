using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefendBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxDefendPoint(float ap)
    {
        slider.maxValue = ap;
        slider.value = 0;
    }

    public void SetDefendPoint(float ap)
    {
        slider.value = ap;
    }
}
