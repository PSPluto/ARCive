using UnityEngine;

namespace Item.Data.ItemDataDerived
{
    [CreateAssetMenu(fileName = "New Stackable Item", menuName = "Scriptable Objects/Item Data/Stackable", order = 0)]
    public class DurableData : ItemData
    {
        [SerializeField] private int maxDurability = 40;
        
        public int MaxDurability => maxDurability;
    }
}