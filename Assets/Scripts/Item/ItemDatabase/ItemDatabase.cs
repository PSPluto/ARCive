using System.Collections.Generic;
using UnityEngine;

namespace Item.Database
{
    [CreateAssetMenu(fileName = "New ItemDatabase", menuName = "Scriptable Objects/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        private bool _isInitialized;
        [SerializeField] private List<ItemData> itemDataList = new();
        private Dictionary<string, ItemData> _itemMap;
        
        /// <summary>
        /// Dictionary構築。
        /// </summary>
        private void Initialize()
        {
            _itemMap = new Dictionary<string, ItemData>();

            foreach (var itemData in itemDataList)
            {
                if (!_itemMap.TryAdd(itemData.ID, itemData))
                {
                    Debug.LogError($"ItemDatabase内で IDが重複しています : {itemData.ID}");
                }
            }
        }

        public ItemData Get(string id)
        {
            if (_isInitialized == false)
            {
                Initialize();
                _isInitialized = true;
            }
            var isSuccess = _itemMap.TryGetValue(id, out var result);
            if (isSuccess)
            {
                return result;
            }
            else
            {
                Debug.Log("GetValueOrDefault が失敗しました。存在しないidが検索されました。");
            }
            return null;
        }
    }
}