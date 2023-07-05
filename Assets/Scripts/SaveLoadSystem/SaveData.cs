using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public List<string> collectedItem;
    public SerializableDictionary<string, ItemPickUpSaveData> activeItems;
    public SerializableDictionary<string, InventorySaveData> chestDictionary;
    public PlayerData CurrentSaveData;

    public InventorySaveData playerInventory;
    public EquipmentSaveData equipmentInventory;

    public SaveData()
    {
        collectedItem = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
        chestDictionary = new SerializableDictionary<string, InventorySaveData> ();
        playerInventory = new InventorySaveData ();
        CurrentSaveData = new PlayerData();
        equipmentInventory = new EquipmentSaveData ();
    }
}
