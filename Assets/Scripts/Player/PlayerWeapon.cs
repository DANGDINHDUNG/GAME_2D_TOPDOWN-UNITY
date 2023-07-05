using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer weapon;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        inventoryHolder = GetComponentInParent<InventoryHolder>();
        animator = GetComponent<Animator>();
        weapon = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerMovement.facingDown || playerMovement.facingRight)
        {
            animator.SetInteger("State", 0);
        }
        else if (playerMovement.facingUp || playerMovement.facingLeft)
        {
            animator.SetInteger("State", 1);
        }

        if (inventoryHolder.EquipmentInventorySystem.InventorySlots[0].ItemData != null)
        {
            weapon.sprite = inventoryHolder.EquipmentInventorySystem.InventorySlots[0].ItemData.Icon;
        }
        else
        {
            weapon.sprite = null;
        }

    }

    public void AddWeaponBuff(EquipmentSlotUI clickedSlot, MouseItemData mouseItemData)
    {
        if (mouseItemData.AssignedInventorySlot.ItemData == null && clickedSlot.AssignedInventorySlot.ItemData != null)
        {
            PlayerStatusController.GetInstance().attackPlus += clickedSlot.AssignedInventorySlot.ItemData.attackPlus;
            PlayerStatusController.GetInstance().healthPlus += clickedSlot.AssignedInventorySlot.ItemData.healthPlus;
            PlayerStatusController.GetInstance().defendPlus += clickedSlot.AssignedInventorySlot.ItemData.defendPlus;
            PlayerStatusController.GetInstance().manaPlus += clickedSlot.AssignedInventorySlot.ItemData.manaPlus;
        }
        else if (mouseItemData.AssignedInventorySlot.ItemData != null && clickedSlot.AssignedInventorySlot.ItemData != null)
        {
            PlayerStatusController.GetInstance().attackPlus = PlayerStatusController.GetInstance().attackPlus - mouseItemData.AssignedInventorySlot.ItemData.attackPlus + clickedSlot.AssignedInventorySlot.ItemData.attackPlus;
            PlayerStatusController.GetInstance().healthPlus = PlayerStatusController.GetInstance().healthPlus + clickedSlot.AssignedInventorySlot.ItemData.healthPlus - mouseItemData.AssignedInventorySlot.ItemData.healthPlus;
            PlayerStatusController.GetInstance().defendPlus = PlayerStatusController.GetInstance().defendPlus + clickedSlot.AssignedInventorySlot.ItemData.defendPlus - mouseItemData.AssignedInventorySlot.ItemData.defendPlus;
            PlayerStatusController.GetInstance().manaPlus = PlayerStatusController.GetInstance().manaPlus + clickedSlot.AssignedInventorySlot.ItemData.manaPlus - mouseItemData.AssignedInventorySlot.ItemData.manaPlus;

        }
        else if (mouseItemData.AssignedInventorySlot.ItemData != null && clickedSlot.AssignedInventorySlot.ItemData == null)
        {
            PlayerStatusController.GetInstance().attackPlus -= mouseItemData.AssignedInventorySlot.ItemData.attackPlus;
            PlayerStatusController.GetInstance().healthPlus -= mouseItemData.AssignedInventorySlot.ItemData.healthPlus;
            PlayerStatusController.GetInstance().defendPlus -= mouseItemData.AssignedInventorySlot.ItemData.defendPlus;
            PlayerStatusController.GetInstance().manaPlus -= mouseItemData.AssignedInventorySlot.ItemData.manaPlus;
        }



    }

    
}
