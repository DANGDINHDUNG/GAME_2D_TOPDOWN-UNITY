using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] private List<CraftingIngredient> _ingredients;
    [SerializeField] private InventoryItemData _craftItem;
    [SerializeField, Min(1)] private int _craftedAmount = 1;
    [SerializeField] private int _craftedFee;

    public List<CraftingIngredient > Ingredients => _ingredients;
    public InventoryItemData CraftItem => _craftItem;
    public int CraftedAmount => _craftedAmount; 
    public int CraftedFee => _craftedFee;
}


[System.Serializable]
public struct CraftingIngredient
{
    public InventoryItemData ItemRequired;
    public int AmountRequired;

    public CraftingIngredient(InventoryItemData itemRequired, int amountRequired, int moneyRequired)
    {
        ItemRequired = itemRequired;
        AmountRequired = amountRequired;
    }
}
