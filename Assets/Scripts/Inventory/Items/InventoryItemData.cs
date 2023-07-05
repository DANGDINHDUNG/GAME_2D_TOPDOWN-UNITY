using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// This is a scriptable object, that defines whazt an item is in our game.
/// It could be inherited from to have branched version of item, for example potions and equipment
/// </summary>
[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int iD = -1;
    public string displayName;
    [TextArea(4, 4)]
    public string description;
    public ItemType itemType = ItemType.NoType;
    public Sprite Icon;
    public int MaxStackSize;
    public int GoldValue;
    public int SellValue;
    public float attackPlus;
    public float healthPlus;
    public float defendPlus;
    public float manaPlus;
    public float spAttackPlus;
    public float attackSpeedPlus;

    public int healthRecovery;
    public int manaRecovery;
}


public enum ItemType
{
    NoType,
    Resource,
    Weapon,
    Potion,
    Food
};

#if UNITY_EDITOR
[CustomEditor(typeof(InventoryItemData)), CanEditMultipleObjects]
public class ItemDataEditor : Editor
{

    public override void OnInspectorGUI()
    {
        InventoryItemData itemData = (InventoryItemData)target;

        // Display always
        itemData.iD = EditorGUILayout.IntField("Item ID", itemData.iD);
        itemData.displayName = EditorGUILayout.TextField("Display Name", itemData.displayName);
        itemData.description = EditorGUILayout.TextField("Description", itemData.description);

        itemData.Icon = EditorGUILayout.ObjectField(itemData.Icon, typeof(Sprite), true, GUILayout.Height(48), GUILayout.Width(48)) as Sprite;

        // Display dropdown
        itemData.itemType = (ItemType)EditorGUILayout.EnumPopup("ItemType", itemData.itemType);

        // Display conditional for two
        if (itemData.itemType == ItemType.Potion)
        {
            itemData.healthRecovery = EditorGUILayout.IntField("Health Recovery", itemData.healthRecovery);
            itemData.manaRecovery = EditorGUILayout.IntField("Mana Recovery", itemData.manaRecovery);
        }
        // Display conditional for one	
        if (itemData.itemType == ItemType.Weapon)
        {
            itemData.attackPlus = EditorGUILayout.FloatField("Attack Plus", itemData.attackPlus);
            itemData.healthPlus = EditorGUILayout.FloatField("Health Plus", itemData.healthPlus);
            itemData.defendPlus = EditorGUILayout.FloatField("Defend Plus", itemData.defendPlus);
            itemData.manaPlus = EditorGUILayout.FloatField("Mana Plus", itemData.manaPlus);
            itemData.spAttackPlus = EditorGUILayout.FloatField("Sp.Attack Plus", itemData.spAttackPlus);
            itemData.attackSpeedPlus = EditorGUILayout.FloatField("Attack Speed Plus", itemData.attackSpeedPlus);

        }


        itemData.MaxStackSize = EditorGUILayout.IntField("Max Stack Size", itemData.MaxStackSize);
        itemData.GoldValue = EditorGUILayout.IntField("Gold Value", itemData.GoldValue);
        itemData.SellValue = EditorGUILayout.IntField("Sell Value", itemData.SellValue);


    }
}
#endif

