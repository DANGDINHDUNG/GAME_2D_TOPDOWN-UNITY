using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(UniqueID))]
[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour
{
    public InventoryItemData ItemData;

    private Collider2D col2D;

    //[SerializeField] private ItemPickUpSaveData itemSaveData;
    private string id;

    private void Awake()
    {
        //id = GetComponent<UniqueID>().ID;
        //SaveLoad.OnLoadGame += LoadGame;
        //itemSaveData = new ItemPickUpSaveData(ItemData, transform.position);

        col2D = GetComponent<Collider2D>();
        col2D.isTrigger = true;
    }

    //private void Start()
    //{
    //    SaveGameManager.data.activeItems.Add(id, itemSaveData);
    //}

    //private void LoadGame(SaveData data)
    //{
    //    if (data.collectedItem.Contains(id)) Destroy(this.gameObject);

    //}

    //private void OnDestroy()
    //{
    //    if (SaveGameManager.data.activeItems.ContainsKey(id)) SaveGameManager.data.activeItems.Remove(id);
    //    SaveLoad.OnLoadGame -= LoadGame;
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if (!inventory) return;

        if (inventory.AddToInventory(ItemData, 1))
        {
            //SaveGameManager.data.collectedItem.Add(id);
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable] 
public struct ItemPickUpSaveData
{
    public InventoryItemData ItemData;
    public Vector3 Position;

    public ItemPickUpSaveData(InventoryItemData itemData, Vector3 position)
    {
        ItemData = itemData;
        Position = position;
    }
}
