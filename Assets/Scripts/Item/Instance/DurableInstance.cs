using Item.Database;
using Item.Database.ItemDataDerived;
using UnityEngine;

namespace Item.Instance
{
    public class DurableInstance :ItemInstance
    {
        public int CurrentDurability { get; private set; }

        public void ChangeDurabilityValue(int value)
        {
            CurrentDurability = Mathf.Clamp(value,0, ((DurableData)(ItemDatabaseManager.Instance.Database.Get(ID))).MaxDurability);
        }
        
        public DurableInstance(string id, int currentDurability) : base(id, 1)
        {
            CurrentDurability = Mathf.Clamp(currentDurability,0, ((DurableData)(ItemDatabaseManager.Instance.Database.Get(id))).MaxDurability);
        }
        
        // TODO: DBの該当SOへの参照をキャッシュする
    }
}