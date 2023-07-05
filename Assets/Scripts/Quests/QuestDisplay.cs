using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    [Header("Recipe List Window")]
    [SerializeField] private GameObject _questListWindow;
    [SerializeField] private QuestList_UI _questListUI;

    [Header("Ingredient Window")]
    [SerializeField] private IngredientSlot_UI _ingredientPrefabs;
    [SerializeField] private Transform _ingredientGrid;
    [SerializeField] private Button _craftButton;
    [SerializeField] private TextMeshProUGUI _playerGoldText;

    [Header("Item Display Section")]
    [SerializeField] private TextMeshProUGUI _itemPreviewName;
    [SerializeField] private TextMeshProUGUI _itemPreviewDescription;

    [SerializeField] private PlayerInventoryHolder _playerInventory;

    private QuestBoard _questBoard;
    private QuestInformation _chosenQuest;

    private void Awake()
    {
        _craftButton.onClick.AddListener(Crafting);
        _craftButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        _playerGoldText.text = _playerInventory.PrimaryInventorySystem.Gold.ToString();
    }

    private void Crafting()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();

        Debug.Log("Craft");
        if (_playerInventory != null)
        {
            Debug.Log("Not null");

            if (CheckIfCanCraft())
            {
                foreach (var ingredient in _chosenQuest.Ingredients)
                {
                    _playerInventory.PrimaryInventorySystem.RemoveItemsFromInventory(ingredient.ItemRequired, ingredient.AmountRequired);
                }
                _playerInventory.PrimaryInventorySystem.EarnGold(_chosenQuest.Gold);
                PlayerStatusController.GetInstance().playerCurrentLevelPoint += _chosenQuest.EXP;
            }
        }
    }

    private bool CheckIfCanCraft()
    {
        Debug.Log("CanCraft");

        var itemsHeld = _playerInventory.PrimaryInventorySystem.GetAllItemsHeld();

        foreach (var ingredient in _chosenQuest.Ingredients)
        {
            if (!itemsHeld.TryGetValue(ingredient.ItemRequired, out int amountHeld)) return false;

            //if (amountHeld < ingredient.AmountRequired)
            //{
            //    Debug.Log("Not enough amount");
            //    return false;
            //}
        }

        return true;
    }

    internal void DisplayQuestWindow(QuestBoard questBoard)
    {
        _questBoard = questBoard;

        ClearItemPreview();
        RefreshListDisplay();
    }

    private void RefreshListDisplay()
    {
        ClearSlot(_questListWindow.transform);

        foreach (var recipe in _questBoard.CurrentQuest)
        {
            var recipeSlot = Instantiate(_questListUI, _questListWindow.transform);
            recipeSlot.Init(recipe, this);
        }
    }

    private void ClearSlot(Transform transformToDestroy)
    {
        foreach (var item in transformToDestroy.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    private void ClearItemPreview()
    {
        _itemPreviewName.text = "";
        _itemPreviewDescription.text = "";
    }

    private void DisplayQuestPreview(QuestInformation data)
    {
        _itemPreviewName.text = data.QuestName;
        _itemPreviewDescription.text = data.QuestDescription;
    }



    public void UpdateChosenQuest(QuestInformation _quest)
    {
        _chosenQuest = _quest;
        DisplayQuestPreview(_chosenQuest);
        _craftButton.gameObject.SetActive(true);
        RefreshQuestWindow();
    }

    private void RefreshQuestWindow()
    {
        ClearSlot(_ingredientGrid);
        foreach (var ingredient in _chosenQuest.Ingredients)
        {
            var ingredientSlot = Instantiate(_ingredientPrefabs, _ingredientGrid.transform);
            ingredientSlot.Init(ingredient.ItemRequired, ingredient.AmountRequired);
            if (CheckIfCanCraft())
            {
                ingredientSlot.EnoughIngredient();
            }
        }

    }
}
