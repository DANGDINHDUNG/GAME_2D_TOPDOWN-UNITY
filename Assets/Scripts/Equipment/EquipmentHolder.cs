using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EquipmentHolder : EquipmentDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] protected EquipmentSlotUI slotPrefab;

    protected override void Start()
    {
        base.Start();

        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.EquipmentInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");

        AssignSlot(inventorySystem);
    }



    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<EquipmentSlotUI, InventorySlots>();

        for (int i = 0; i < 1; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }
}

[System.Serializable]
public struct EquipmentSaveData
{
    public InventorySystem InvSystem;
    public Vector3 Position;

    public EquipmentSaveData(InventorySystem invSystem, Vector3 position)
    {
        InvSystem = invSystem;
        Position = position;
    }

    public EquipmentSaveData(InventorySystem invSystem)
    {
        InvSystem = invSystem;
        Position = Vector3.zero;
    }


}
