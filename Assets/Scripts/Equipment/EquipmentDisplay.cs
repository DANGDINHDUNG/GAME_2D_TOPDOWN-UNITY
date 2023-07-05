using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class EquipmentDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<EquipmentSlotUI, InventorySlots> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<EquipmentSlotUI, InventorySlots> SlotDictionary => slotDictionary;

    [SerializeField] private PlayerInventoryHolder _playerInventoryHolder;
    [SerializeField] private PlayerWeapon playerWeapon;


    protected virtual void Start()
    {
        _playerInventoryHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerWeapon>();
    }
    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlots updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot)      // Slot value = the "under the hood" inventory slot
            {
                slot.Key.UpdateUISlot(updatedSlot); // Slot key - the UI representation of the value
            }
        }
    }

    public void SlotClicked(EquipmentSlotUI clickedUISlot)
    {
        // Clicked slot has an item - mouse doesn't have an item - pick up that item

        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

            clickedUISlot.ClearSlot();
            playerWeapon.AddWeaponBuff(clickedUISlot, mouseInventoryItem);
            return;
        }

        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            return;
        }



        // Clicked slot doesn't have an item - Mouse does have an item - place the mouse item into the empty slot

        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData.itemType == ItemType.Weapon)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();
            mouseInventoryItem.ClearSlot();
            playerWeapon.AddWeaponBuff(clickedUISlot, mouseInventoryItem);
            return;
        }

        // Both slots have an item - decide what to do . . .
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData.itemType == ItemType.Weapon)
        {
            SwapSlot(clickedUISlot);
            playerWeapon.AddWeaponBuff(clickedUISlot, mouseInventoryItem);
            return;
        }
    }

    private void SwapSlot(EquipmentSlotUI clickedUISlot)
    {
        var cloneSlot = new InventorySlots(mouseInventoryItem.AssignedInventorySlot.ItemData, mouseInventoryItem.AssignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(cloneSlot);
        clickedUISlot.UpdateUISlot();
    }
}
