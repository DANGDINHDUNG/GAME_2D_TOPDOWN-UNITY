using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace DPUtils.Systems.ItemSystem.Scriptable_Objects.Items.Resources
{
    [CreateAssetMenu(menuName = "Inventory System/Item Database")]
    public class Database : ScriptableObject
    {
        [SerializeField] private List<InventoryItemData> _itemDatabase;

        public List<InventoryItemData> ItemDatabase => _itemDatabase;

        [ContextMenu("Set IDs")]
        public void SetItemIDs()
        {
            _itemDatabase = new List<InventoryItemData>();

            var foundItems = UnityEngine.Resources.LoadAll<InventoryItemData>("ItemData").OrderBy(i => i.iD).ToList();

            var hasIDInRange = foundItems.Where(i => i.iD != -1 && i.iD < foundItems.Count).OrderBy(i => i.iD).ToList();
            var hasIDNotInRange = foundItems.Where(i => i.iD != -1 && i.iD >= foundItems.Count).OrderBy(i => i.iD).ToList();
            var noID = foundItems.Where(i => i.iD == -1).ToList();

            var index = 0;
            for (int i = 0; i < foundItems.Count; i++)
            {
                Debug.Log($"Checking ID {i}");
                var itemToAdd = hasIDInRange.Find(d => d.iD == i);

                if (itemToAdd != null)
                {
                    Debug.Log($"Found item {itemToAdd} which has an iD of {itemToAdd.iD}");
                    _itemDatabase.Add(itemToAdd);
                }
                else if (index < noID.Count)
                {
                    noID[index].iD = i;
                    Debug.Log($"Setting item {noID[index]} which has an invaliD iD to the iD of {i}");
                    itemToAdd = noID[index];
                    index++;
                    _itemDatabase.Add(itemToAdd);
                }
#if UNITY_EDITOR
                if (itemToAdd) EditorUtility.SetDirty(itemToAdd);
#endif

            }

            foreach (var item in hasIDNotInRange)
            {
                _itemDatabase.Add(item);
#if UNITY_EDITOR
                if (item) EditorUtility.SetDirty(item);
#endif
            }
#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
#endif


        }

        public InventoryItemData GetItem(int iD)
        {
            return _itemDatabase.Find(i => i.iD == iD);
        }

        public InventoryItemData GetItem(string displayName)
        {
            return _itemDatabase.Find(i => i.displayName == displayName);
        }
    }
}
