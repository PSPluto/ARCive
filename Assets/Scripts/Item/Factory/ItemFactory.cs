using UnityEngine;
using Item.Database;

namespace Item
{
    public class ItemFactory
    {
        private readonly LootTableSelector _lootTableSelector = new LootTableSelector();

        /// <summary>
        /// lootTableと戦利品価値の重み から実際にアイテムを生成してリターンします。
        /// </summary>
        /// <param name="lootTable"></param>
        /// <param name="lootValueBias"></param>
        /// <returns></returns>
        public Instance.ItemInstance CreateItemInstance(LootTable lootTable, float lootValueBias)
        {
            if (lootTable == null || lootTable.Entries == null || lootTable.Entries.Count == 0)
            {
                Debug.LogError("引数 lootTable の値が nullまたはempty で CreateItemInstance が呼び出されました。");
                return null;
            }

            var hitId = _lootTableSelector.SelectItemId(lootTable, lootValueBias);

            if (ItemDatabaseManager.Instance.Database.TryGet(hitId, out var itemData))
            {
                return itemData.CreateInstance();
            }
            else
            {
                Debug.LogError($"ItemData が登録されていないアイテム「{hitId}」をインスタンス化しようとしました。");
                return null;
            }
        }
    }
}