using UnityEngine;

namespace Item
{
    public class ItemFactory
    {
        [SerializeField] private ItemInstanceFactoryDatabase _instanceFactoryDatabase;
        /// <summary>
        /// lootTableと戦利品価値の重み から実際にアイテムを生成してリターンします。
        /// </summary>
        /// <param name="lootTable"></param>
        /// <param name="lootValueWeight"></param>
        /// <returns></returns>
        public Instance.ItemInstance CreateItemInstance(LootTable lootTable, float lootValueWeight)
        {
            // TODO: リターンでラムダ式を返す。
            return null;
        }
    }
}