using Item.Instance;
using UnityEngine;

namespace Item.Database.ItemDataDerived
{
    [CreateAssetMenu(
        fileName = "New Basic Item",
        menuName = "Scriptable Objects/Item Data/Basic",
        order = 0)]
    public class BasicData : ItemData
    {
        [Min(1)] [SerializeField] private int initialStackCount = 1;

        public override ItemInstance CreateInstance()
        {
            return new ItemInstance(this, initialStackCount);
        }
    }
}