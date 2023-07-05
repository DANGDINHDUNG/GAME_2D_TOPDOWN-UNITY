using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private ShopkeeperDisplay _shopkeeperDisplay;
    [SerializeField] private CraftingDisplay _craftingDisplay;
    [SerializeField] private QuestDisplay _questDisplay;
    [SerializeField] public bool isShopOpen = false;

    private void Awake()
    {
        _shopkeeperDisplay.gameObject.SetActive(false);
        _craftingDisplay.gameObject.SetActive(false);
        _questDisplay.gameObject.SetActive(false);
        isShopOpen = false;
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequest += DisplayShopWindow;
        CraftingBench.OnCraftingDisplayRequested += DisplayCraftingWindow;
        QuestBoard.OnQuestDisplayRequested += DisplayQuestWindow;
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequest -= DisplayShopWindow;
        CraftingBench.OnCraftingDisplayRequested -= DisplayCraftingWindow;
        QuestBoard.OnQuestDisplayRequested -= DisplayQuestWindow;

    }

    void Update()
    {
        if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;
        if (_shopkeeperDisplay.gameObject.activeInHierarchy)
        {
            _shopkeeperDisplay.gameObject.SetActive(false);
            isShopOpen = false;
        }

        if (_craftingDisplay.gameObject.activeInHierarchy)
        {
            _craftingDisplay.gameObject.SetActive(false);
        }

        if (_questDisplay.gameObject.activeInHierarchy)
        {
            _questDisplay.gameObject.SetActive(false);
        }


    }

    private void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventory)
    {
        _shopkeeperDisplay.gameObject.SetActive(true);
        isShopOpen = true;
        _shopkeeperDisplay.DisplayShopWindow(shopSystem, playerInventory);
    }

    private void DisplayCraftingWindow(CraftingBench craftingBench)
    {
        _craftingDisplay.gameObject.SetActive(true);
        _craftingDisplay.DisplayCraftingWindow(craftingBench);
    }


    private void DisplayQuestWindow(QuestBoard questBoard)
    {
        _questDisplay.gameObject.SetActive(true);
        _questDisplay.DisplayQuestWindow(questBoard);
    }
}
