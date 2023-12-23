using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quests")]
public class QuestInformation : ScriptableObject
{
    [SerializeField] private string questName;
    [SerializeField] private List<QuestIngredient> _ingredients;
    [SerializeField, TextArea(4,4)] private string _questDescription;
    [SerializeField] private int gold;
    [SerializeField] private int exp;

    public string QuestName => questName;
    public List<QuestIngredient> Ingredients => _ingredients;
    public string QuestDescription => _questDescription;
    public int Gold => gold;
    public int EXP => exp;
}


[System.Serializable]
public struct QuestIngredient
{
    public InventoryItemData ItemRequired;
    public int AmountRequired;

    public QuestIngredient(InventoryItemData itemRequired, int amountRequired)
    {
        ItemRequired = itemRequired;
        AmountRequired = amountRequired;
    }
}

