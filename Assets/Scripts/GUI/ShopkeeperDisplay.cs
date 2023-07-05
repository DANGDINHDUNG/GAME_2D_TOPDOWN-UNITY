using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopkeeperDisplay : MonoBehaviour
{
    [SerializeField] private ShopSlotUI _shopSlotPrefab;
    [SerializeField] private TextMeshProUGUI _playerGoldText;

    [SerializeField] private GameObject _itemListContentPanel;


    private ShopSystem _shopSystem;
    private PlayerInventoryHolder _playerInventoryHolder;

    private void Update()
    {
        _playerGoldText.text = _playerInventoryHolder.PrimaryInventorySystem.Gold.ToString();
    }

    public void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventoryHolder)
    {
        _shopSystem = shopSystem;
        _playerInventoryHolder = playerInventoryHolder;
        _playerGoldText.text = _playerInventoryHolder.PrimaryInventorySystem.Gold.ToString();

        RefreshDisplay();

        DisplayShopInventory();
    }

    private void RefreshDisplay()
    {
        ClearSlot();
    }

    private void ClearSlot()
    {
        foreach (var item in _itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    private void DisplayShopInventory()
    {
        foreach (var item in _shopSystem.ShopInventory)
        {
            if (item.ItemData == null) continue;

            var shopSlot = Instantiate(_shopSlotPrefab, _itemListContentPanel.transform);
            shopSlot.Init(item, _shopSystem.BuyMarkUp);
        }
    }

    public void BuyItem(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;
        if (_playerInventoryHolder.PrimaryInventorySystem.Gold < data.GoldValue)
        {
            StartCoroutine(NotEnoughGold());
        }
        else
        {
            _playerInventoryHolder.AddToInventory(data, 1);
            _playerInventoryHolder.PrimaryInventorySystem.SpendGold(data.GoldValue);
        }
    }

    IEnumerator NotEnoughGold()
    {
        _playerGoldText.color = Color.red;

        yield return new WaitForSeconds((float)0.1);

        _playerGoldText.color = new Color32(91, 42, 41, 255);
    }
}
