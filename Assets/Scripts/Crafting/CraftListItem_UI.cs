using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftListItem_UI : MonoBehaviour
{
    [SerializeField] private Image _recipeSprite;
    [SerializeField] private TextMeshProUGUI _recipeName;
    [SerializeField] private TextMeshProUGUI _recipeFee;

    [SerializeField] private Button _button;

    private CraftingDisplay _parentDisplay;
    private CraftingRecipe _recipe;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void Init(CraftingRecipe recipe, CraftingDisplay parentDisplay)
    {
        _parentDisplay = parentDisplay;
        _recipe = recipe;
        _recipeSprite.sprite = _recipe.CraftItem.Icon;
        _recipeName.text = _recipe.CraftItem.displayName;
        _recipeFee.text = _recipe.CraftedFee.ToString();
    }

    public void OnButtonClick()
    {
        if (_parentDisplay == null) return;

        _parentDisplay.UpdateChosenRecipe(_recipe);
    }
}
