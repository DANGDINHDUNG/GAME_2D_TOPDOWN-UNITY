using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpAttackBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSpAttackPoint(float ap)
    {
        slider.maxValue = ap;
        slider.value = 0;
    }

    public void SetSpAttackPoint(float ap)
    {
        slider.value = ap;
    }
}
