using UnityEngine;

namespace Item.Data.ItemDataDerived
{
    [CreateAssetMenu(fileName = "New Stackable Item", menuName = "Scriptable Objects/Item Data/Stackable", order = 0)]
    public class StackableData : ItemData
    {
        [SerializeField] private int maxStackSize = 1;
        
        public int MaxStackSize => maxStackSize;
    }
}