using DPUtils.Systems.ItemSystem.Scriptable_Objects.Items.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class InventorySlots : ItemSlot
{
    public InventorySlots(InventoryItemData source, int amount)     // Constructor to make a occupied inventory slot
    {
        itemData = source;
        _itemID = itemData.iD;
        stackSize = amount;
    }

    public InventorySlots()     // Constructor to make an empty inventory slot
    {
        ClearSlot();
    }


    public void UpdateInventorySlot(InventoryItemData data, int amount)     // Updates slot directly
    {
        itemData = data;
        _itemID = itemData.iD;
        stackSize = amount;
    }

    // Would the be enough room in the stack for the amount we're try to add
    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining)       
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }


    public bool SplitStack(out InventorySlots splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int haftStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(haftStack);

        splitStack = new InventorySlots(itemData, haftStack);
        return true;
    }

    public bool DecreaseStack()
    {
        if (stackSize < 1)
        {
            return false;
        }

        RemoveFromStack(1);
        return true;
    }

}
