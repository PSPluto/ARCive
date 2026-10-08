using UnityEngine;

namespace Item.DataBase.ItemDataDerived
{
    [CreateAssetMenu(fileName = "New Durable Item", menuName = "Scriptable Objects/Item Data/Durable", order = 0)]
    public class DurableData : ItemData
    {
        [SerializeField] private int maxDurability = 40;
        
        public int MaxDurability => maxDurability;
    }
}