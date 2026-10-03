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
        
        public string ID => id;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
    }
}