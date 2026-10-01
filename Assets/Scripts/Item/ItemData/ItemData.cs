using UnityEngine;

namespace Item.ItemData
{
    /// <summary>
    /// 主にアイテムの固定データを扱う。
    /// </summary>
    [CreateAssetMenu(fileName = "New ItemData", menuName = "Scriptable Objects/Item Data")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;
        
        public string ID => id;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
    }
}