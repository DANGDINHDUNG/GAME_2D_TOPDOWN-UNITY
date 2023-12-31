using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{

    public static UnityAction OnPlayerInventoryChanged;

    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;

    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
        SaveGameManager.data.equipmentInventory = new EquipmentSaveData(equipmentInventorySystem);
    }

    protected override void LoadInventory(SaveData data)
    {
        // Check the save data for this specify chests inventory, and if it exists, load it in.

        if (data.playerInventory.InvSystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.InvSystem;
            OnPlayerInventoryChanged?.Invoke();
        }

        if (data.equipmentInventory.InvSystem != null)
        {
            this.equipmentInventorySystem = data.equipmentInventory.InvSystem;
            OnPlayerInventoryChanged?.Invoke();
        }


    }

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame) { OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset); }
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }

        return false;
    }
}
