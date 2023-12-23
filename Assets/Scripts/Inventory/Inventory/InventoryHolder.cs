using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;
    [SerializeField] protected InventorySystem equipmentInventorySystem;
    [SerializeField] protected int _gold;
    [SerializeField] protected int offset = 10;

    public int Offset => offset;

    public InventorySystem PrimaryInventorySystem => primaryInventorySystem;
    public InventorySystem EquipmentInventorySystem => equipmentInventorySystem;

    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        SaveLoad.OnLoadGame += LoadInventory;

        primaryInventorySystem = new InventorySystem(inventorySize, _gold);
        equipmentInventorySystem = new InventorySystem(offset, _gold);
    }

    protected abstract void LoadInventory(SaveData saveData);    
}

[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem InvSystem;
    public Vector3 Position;

    public InventorySaveData(InventorySystem _invSystem, Vector3 _position)
    {
        InvSystem = _invSystem;
        Position = _position;
    }

    public InventorySaveData(InventorySystem _invSystem)
    {
        InvSystem = _invSystem;
        Position = Vector3.zero;
    }
}
