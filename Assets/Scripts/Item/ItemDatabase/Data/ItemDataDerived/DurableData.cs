using UnityEngine;
using Item.Instance;

namespace Item.Database.ItemDataDerived
{
    [CreateAssetMenu(fileName = "New Durable Item", menuName = "Scriptable Objects/Item Data/Durable", order = 0)]
    public class DurableData : ItemData
    {
        [Min(1)] [SerializeField] private int maxDurability = 40;
        [Min(0)] [SerializeField] private int initialDurability = 40;

        public int MaxDurability => maxDurability;

        public override ItemInstance CreateInstance()
        {
            return new DurableInstance(this, initialDurability);
        }
    }
}