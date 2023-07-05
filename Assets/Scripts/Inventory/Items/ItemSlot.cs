using DPUtils.Systems.ItemSystem.Scriptable_Objects.Items.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSlot : ISerializationCallbackReceiver
{
    [NonSerialized] protected InventoryItemData itemData;            // References to the data
    [SerializeField] protected int _itemID = -1;
    [SerializeField] protected int stackSize;                         // Current stack size - how many of data do we have?

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;


    public void ClearSlot()     // Clear the slot
    {
        itemData = null;
        _itemID = -1;
        stackSize = -1;
    }


    public void AssignItem(InventorySlots invSlot)          // Assigns an item to the slot
    {
        // Does the slot contain the same item? Add to stack if so
        if (itemData == invSlot.ItemData) AddToStack(invSlot.stackSize);
        else  // Overwrite slot with the inventory slot that we're passing in 
        {
            itemData = invSlot.itemData;
            _itemID = ItemData.iD;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }

    }

    public void AssignItem(InventoryItemData data, int amount)
    {
        if (ItemData == data) AddToStack(amount);
        else
        {
            itemData = data;
            _itemID = data.iD;
            stackSize = 0;
            AddToStack(amount);
        }
    }


    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
        if (stackSize < 1) ClearSlot();
    }

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        if (_itemID == -1) return;

        var db = Resources.Load<Database>("Database");
        itemData = db.GetItem(_itemID);
    }
}
