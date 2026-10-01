using System.Collections.Generic;
using UnityEngine;

namespace Item.ItemData
{
    [CreateAssetMenu(fileName = "New ItemDatabase", menuName = "Scriptable Objects/Item Database")]
    public class ItemDataBase : ScriptableObject
    {
        [SerializeField] private List<ItemData> itemDataList = new();
        private Dictionary<string, ItemData> _itemMap;
        
        /// <summary>
        /// Dictionary構築。
        /// </summary>
        public void Initialize()
        {
            _itemMap = new Dictionary<string, ItemData>();

            foreach (var itemData in itemDataList)
            {
                _itemMap.TryAdd(itemData.ID, itemData);
            }
        }

        public ItemData Get(string id)
        { 
            return _itemMap.GetValueOrDefault(id, null);
        }
    }
}