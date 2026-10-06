using UnityEngine;

namespace Item.Data
{
    /// <summary>
    /// 主にアイテムの固定データを扱う。
    /// </summary>
    [CreateAssetMenu(fileName = "New ItemData", menuName = "Scriptable Objects/Item Data")]
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private string id;
        
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;
        
        [SerializeField] private ItemRarity itemRarity;
        
        public string ID => id;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public ItemRarity ItemRarity => itemRarity;
    }

    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    }
}