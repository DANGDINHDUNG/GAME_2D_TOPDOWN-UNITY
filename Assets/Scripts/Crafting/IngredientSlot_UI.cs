using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;

    public void Init(InventoryItemData data, int amount)
    {
        itemSprite.preserveAspect = true;
        itemSprite.sprite = data.Icon;
        itemSprite.color = Color.white;
        itemCount.text = amount.ToString();
        itemCount.color = Color.red;
    }

    public void EnoughIngredient()
    {
        itemCount.color = Color.white;
    }
}
