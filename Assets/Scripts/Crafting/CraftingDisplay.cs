using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftingDisplay : MonoBehaviour
{
    [Header("Recipe List Window")]
    [SerializeField] private GameObject _recipeListWindow;
    [SerializeField] private CraftListItem_UI _craftListItemUI;

    [Header("Ingredient Window")]
    [SerializeField] private IngredientSlot_UI _ingredientPrefabs;
    [SerializeField] private Transform _ingredientGrid;
    [SerializeField] private Button _craftButton;
    [SerializeField] private TextMeshProUGUI _playerGoldText;

    [Header("Item Display Section")]
    [SerializeField] private Image _itemPreviewSprite;
    [SerializeField] private TextMeshProUGUI _itemPreviewName;
    [SerializeField] private TextMeshProUGUI _itemPreviewDescription;

    [SerializeField] private PlayerInventoryHolder _playerInventory;

    private CraftingBench _craftingBench;
    private CraftingRecipe _chosenRecipe;

    private void Awake()
    {
        _craftButton.onClick.AddListener(Crafting);
        _craftButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        _playerGoldText.text = _playerInventory.PrimaryInventorySystem.Gold.ToString();
    }

    private void Crafting()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();

        Debug.Log("Craft");
        if (_playerInventory != null)
        {
            Debug.Log("Not null");

            if (CheckIfCanCraft())
            {
                foreach (var ingredient in _chosenRecipe.Ingredients)
                {
                    _playerInventory.PrimaryInventorySystem.RemoveItemsFromInventory(ingredient.ItemRequired, ingredient.AmountRequired);
                }
                _playerInventory.PrimaryInventorySystem.SpendGold(_chosenRecipe.CraftedFee);
                _playerInventory.PrimaryInventorySystem.AddToInventory(_chosenRecipe.CraftItem, _chosenRecipe.CraftedAmount);
            }
        }
    }

    private bool CheckIfCanCraft()
    {
        Debug.Log("CanCraft");

        var itemsHeld = _playerInventory.PrimaryInventorySystem.GetAllItemsHeld();

        if (_playerInventory.PrimaryInventorySystem.Gold < _chosenRecipe.CraftedFee)
        {
            StartCoroutine(NotEnoughGold());
            return false; }

        foreach (var ingredient in _chosenRecipe.Ingredients)
        {
            if (!itemsHeld.TryGetValue(ingredient.ItemRequired, out int amountHeld)) return false;

            //if (amountHeld < ingredient.AmountRequired)
            //{
            //    Debug.Log("Not enough amount");
            //    return false;
            //}
        }

        return true;
    }

    internal void DisplayCraftingWindow(CraftingBench craftingBench)
    {
        _craftingBench = craftingBench;

        ClearItemPreview();
        RefreshListDisplay();
    }

    private void RefreshListDisplay()
    {
        ClearSlot(_recipeListWindow.transform);

        foreach (var recipe in _craftingBench.KnownRecipe)
        {
            var recipeSlot = Instantiate(_craftListItemUI, _recipeListWindow.transform);
            recipeSlot.Init(recipe, this);
        }
    }

    private void ClearSlot(Transform transformToDestroy)
    {
        foreach (var item in transformToDestroy.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    private void ClearItemPreview()
    {
        _itemPreviewSprite.sprite = null;
        _itemPreviewSprite.color = Color.clear;
        _itemPreviewName.text = "";
        _itemPreviewDescription.text = "";
    }

    private void DisplayItemPreview(InventoryItemData data)
    {
        _itemPreviewSprite.sprite = data.Icon;
        _itemPreviewSprite.color = Color.white;
        _itemPreviewName.text = data.displayName;
        _itemPreviewDescription.text = data.description;
    }

    public void UpdateChosenRecipe(CraftingRecipe _recipe)
    {
        _chosenRecipe = _recipe;
        DisplayItemPreview(_chosenRecipe.CraftItem);
        _craftButton.gameObject.SetActive(true);
        RefreshRecipeWindow();
    }

    private void RefreshRecipeWindow()
    {
        ClearSlot(_ingredientGrid);
        foreach (var ingredient in _chosenRecipe.Ingredients)
        {
            var ingredientSlot = Instantiate(_ingredientPrefabs, _ingredientGrid.transform);
            ingredientSlot.Init(ingredient.ItemRequired, ingredient.AmountRequired);
            if (CheckIfCanCraft())
            {
                ingredientSlot.EnoughIngredient();
            }
        }

    }

    IEnumerator NotEnoughGold()
    {
        _playerGoldText.color = Color.red;

        yield return new WaitForSeconds((float)0.1);

        _playerGoldText.color = new Color32(91, 42, 41, 255);
    }
}
