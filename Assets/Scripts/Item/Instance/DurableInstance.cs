using Item.DataBase;
using Item.DataBase.ItemDataDerived;
using UnityEngine;

namespace Item.Instance
{
    public class DurableInstance :ItemInstance
    {
        public int CurrentDurability { get; private set; }

        public void ChangeDurabilityValue(int value)
        {
            CurrentDurability = Mathf.Clamp(value,0, ((DurableData)(ItemDataBaseManager.Instance.DataBase.Get(ID))).MaxDurability);
        }
        
        public DurableInstance(string id, int currentDurability) : base(id, 1)
        {
            CurrentDurability = Mathf.Clamp(currentDurability,0, ((DurableData)(ItemDataBaseManager.Instance.DataBase.Get(id))).MaxDurability); 
        }
        
        // TODO: DBの該当SOへの参照をキャッシュする
    }
}