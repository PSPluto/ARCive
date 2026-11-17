using Item.Database;
using Item.Factory.lootTable;
using UnityEngine;

namespace Item.Factory
{
    public static class ItemFactory
    {
        private static readonly LootTableSelector LootTableSelector = new LootTableSelector();

        /// <summary>
        /// lootTableと戦利品価値の重み から実際にアイテムを生成してリターンします。
        /// </summary>
        /// <param name="lootTable"></param>
        /// <param name="lootValueBias"></param>
        /// <returns></returns>
        public static Instance.ItemInstance CreateItemInstance(LootTable lootTable, float lootValueBias)
        {
            if (lootTable == null || lootTable.Entries == null || lootTable.Entries.Count == 0)
            {
                Debug.LogError("引数 lootTable の値が nullまたはempty で CreateItemInstance が呼び出されました。");
                return null;
            }

            var hitId = LootTableSelector.SelectItemId(lootTable, lootValueBias);

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