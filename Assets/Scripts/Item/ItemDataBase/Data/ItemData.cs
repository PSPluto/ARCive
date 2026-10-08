using UnityEngine;

namespace Item.DataBase
{
    /// <summary>
    /// 主にアイテムの固定データを扱う。
    /// </summary>
    [CreateAssetMenu(fileName = "New ItemData", menuName = "Scriptable Objects/Item Data")]
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private string id = "tmpItem";
        
        [SerializeField] private string itemName = "tmpItem";
        [SerializeField] private string itemDescription = "tmp";
        
        [SerializeField] private ItemRarity itemRarity = ItemRarity.Common;

        [SerializeField] private int maxStackSize = 1;
        
        public string ID => id;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public ItemRarity ItemRarity => itemRarity;
        public int MaxStackSize => maxStackSize;
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