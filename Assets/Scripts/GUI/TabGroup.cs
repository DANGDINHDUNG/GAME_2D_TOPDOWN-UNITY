using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{

    public List<TabButtonUI> tabButtons;
    public Sprite tabIdle;
    public Sprite tabActive;
    public Sprite tabHover;
    public TabButtonUI selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(TabButtonUI button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButtonUI>();
        }

        tabButtons.Add(button);
    }

    // Khi con chuột rê qua nút
    public void OnTabEnter(TabButtonUI button)
    {
        ResetTabs();
        if (selectedTab == null && button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }

    // Khi nút ko được nhấn
    public void OnTabExit(TabButtonUI button)
    {
        ResetTabs();

    }

    // Khi nút được nhấn
    public void OnTabSelected(TabButtonUI button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    //Làm mới nút
    public void ResetTabs()
    {
        foreach (TabButtonUI button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
            {
                continue;
            }
            button.background.sprite = tabIdle;
        }
    }

}
