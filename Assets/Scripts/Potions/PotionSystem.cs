using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotionSystem : MonoBehaviour
{

    public HealthBar healthBar;
    public ManaBar manaBar;

    public void UsePotion(InventorySlot_UI clickedUISlot)
    {
        PlayerStatusController.GetInstance().currentHealth += clickedUISlot.AssignedInventorySlot.ItemData.healthRecovery;
        PlayerStatusController.GetInstance().currentMana += clickedUISlot.AssignedInventorySlot.ItemData.manaRecovery;

        if (PlayerStatusController.GetInstance().currentHealth > PlayerStatusController.GetInstance().playerCurrentHealth)
        {
            PlayerStatusController.GetInstance().currentHealth = PlayerStatusController.GetInstance().playerCurrentHealth;
        }

        if (PlayerStatusController.GetInstance().currentMana > PlayerStatusController.GetInstance().playerCurrentMana)
        {
            PlayerStatusController.GetInstance().currentMana = PlayerStatusController.GetInstance().playerCurrentMana;
        }

        healthBar.SetHealth(PlayerStatusController.GetInstance().currentHealth);
        manaBar.SetMana(PlayerStatusController.GetInstance().currentMana);

        clickedUISlot.AssignedInventorySlot.DecreaseStack();

        DecreaseUsedPotion(clickedUISlot);

    }

    void DecreaseUsedPotion(InventorySlot_UI clickedUISlot)
    {
        clickedUISlot.UpdateUISlot();
        if (clickedUISlot.AssignedInventorySlot.StackSize < 1)
        {
            clickedUISlot.ClearSlot();
        }
    }
}
