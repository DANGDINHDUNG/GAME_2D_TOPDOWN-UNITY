using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHPPoint(float ap)
    {
        slider.maxValue = ap;
        slider.value = 0;
    }

    public void SetHPPoint(float ap)
    {
        slider.value = ap;
    }
}
