using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestList_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questName;
    [SerializeField] private TextMeshProUGUI _questGold;
    [SerializeField] private TextMeshProUGUI _questEXP;

    [SerializeField] private Button _button;

    private QuestDisplay _parentDisplay;
    private QuestInformation _quest;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void Init(QuestInformation quest, QuestDisplay parentDisplay)
    {
        _parentDisplay = parentDisplay;
        _quest = quest;
        _questName.text = _quest.QuestName;
        _questGold.text = _quest.Gold.ToString();
        _questEXP.text = _quest.EXP.ToString();
    }

    public void OnButtonClick()
    {
        if (_parentDisplay == null) return;

        _parentDisplay.UpdateChosenQuest(_quest);
    }
}
