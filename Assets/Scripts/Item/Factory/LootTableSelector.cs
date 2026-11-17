using System.Collections.Generic;
using System.Linq;
using Item.Database;
using Item.Factory.lootTable;
using UnityEngine;

namespace Item.Factory
{
    public class LootTableSelector
    {
        public string SelectItemId(LootTable lootTable, float lootValueBias)
        {
            var weights = lootTable.Entries.Select(entry => entry.weight).ToArray();
            
            if (weights.Any(weight => weight < 0)) 
            {
                Debug.LogError("LootTable に負の weight が含まれています。SOを修正してください");
                return lootTable.Entries[0].id;
            }

            string[] itemIds = lootTable.Entries.Select(entry => entry.id).ToArray();
            ItemRarity[] itemRarities = GetItemRarities(itemIds);
            float[] adjustedWeights = AdjustWeights(weights, itemRarities, lootValueBias);

            float totalWeight = adjustedWeights.Sum();
            float randomValue = Random.Range(0f, totalWeight);
            float currentSum = 0f;

            for (var i = 0; i < adjustedWeights.Length; i++)
            {
                currentSum += adjustedWeights[i];
                if (randomValue < currentSum)
                {
                    return itemIds[i];
                }
            }
            Debug.LogError("LootTable のアイテム選択中にエラー");
            return itemIds[0];
        }

        private static ItemRarity[] GetItemRarities(string[] itemIds) 
        {
            var itemRarities = new ItemRarity[itemIds.Length];
            
            for (var i = 0; i < itemIds.Length; i++)
            {
                if (!ItemDatabaseManager.Instance.Database.TryGet(itemIds[i], out var itemData))
                {
                    throw new KeyNotFoundException(
                        $"ItemDatabase に ID '{itemIds[i]}' が登録されていません。");
                }

                itemRarities[i] = itemData.ItemRarity;
            }
            return itemRarities;
        }

        private static float[] AdjustWeights(float[] weights, ItemRarity[] itemRarities, float lootValueBias)
        {
            var bias = Mathf.Clamp(lootValueBias, -1f, 1f);
            var targetRarity = (bias + 1f) * 0.5f;
            var adjustedWeights = new float[weights.Length];
            var maxRarity = (float)ItemRarity.Legendary;

            for (var i = 0; i < weights.Length; i++)
            {
                var rarity = (float)itemRarities[i] / maxRarity;
                var rarityWeight = Mathf.Clamp01(1f - Mathf.Abs(rarity - targetRarity));
                adjustedWeights[i] = weights[i] * rarityWeight;
            }
            return adjustedWeights;
        }
    }
}