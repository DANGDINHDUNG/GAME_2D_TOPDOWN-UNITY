using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxAttackPoint(float ap)
    {
        slider.maxValue = ap;
        slider.value = 0;
    }

    public void SetAttackPoint(float ap)
    {
        slider.value = ap;
    }
}
