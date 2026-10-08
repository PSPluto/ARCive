using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Item.DataBase;
using Item.Factory;

namespace Item
{
    public class ItemFactory
    {
        private ItemInstanceFactoryDataBase _instanceFactoryDatabase;
        private void Initialize()
        {
            _instanceFactoryDatabase = new ItemInstanceFactoryDataBase();
        }
        
        /// <summary>
        /// lootTableと戦利品価値の重み から実際にアイテムを生成してリターンします。
        /// </summary>
        /// <param name="lootTables"></param>
        /// <param name="lootValueBias"></param>
        /// <returns></returns>
        public Instance.ItemInstance CreateItemInstance(LootTable lootTable, float lootValueBias)
        {
            if (lootTable == null || lootTable.Entries == null || lootTable.Entries.Count == 0 )
            {
                Debug.LogError("引数 lootTable の値が nullまたはempty で CreateItemInstance が呼び出されました。");
                return null;
            } 
            if (_instanceFactoryDatabase == null)
            {
                Initialize();
            }
            // アイテムを決める
            var hitIndex =  SearchIndex();
            var hitId = lootTable.Entries[hitIndex].id;

            if (_instanceFactoryDatabase.TryGetValue(hitId, out var instanceFunc))
            {
                return instanceFunc.Invoke();
            }
            else
            {
                Debug.LogError($"new式の登録されていないアイテム「{hitId}」をインスタンス化しようとしました。");
                return null;
            }
            
            
            
            int SearchIndex()
            {
                var weights = lootTable.Entries.Select(entry => entry.weight).ToArray();
                if (weights.Any(weight => weight < 0))
                {
                    Debug.LogError("LootTable に負の weight が含まれています。SOを修正してください");
                    return 0;
                }
                
                
                // ここに全体重みの処理を書く。
                // IDから価値を引っ張ってきて、「高価さ」によってlootValueBiasを適用する量を個々で決めていく。
                
                var idArray = lootTable.Entries.Select(entry => entry.id).ToArray();
                ItemRarity[] itemRarities = ReturnItemRarities();
                
                // weightをlootValueBias(-1~1)とレアリティ(0~5)に基づいて調整

                var adjustedWeights = ReturnAdjustWeight();

                var totalWeight = adjustedWeights.Sum();
                var randomValue = Random.Range(0f, totalWeight);

                float currentSum = 0f;

                for (var i = 0; i < adjustedWeights.Length; i++)
                {
                    currentSum += adjustedWeights[i];

                    if (randomValue < currentSum)
                    {
                        return i;
                    }
                }
                
                
                Debug.LogError("SearchIndex 内でエラーが発生しました。");
                return 0;

                ItemRarity[] ReturnItemRarities()
                {
                    List<ItemRarity> itemRarityList = new List<ItemRarity>();
                    foreach (var id in idArray)
                    {
                        itemRarityList.Add(ItemDataBaseManager.Instance.DataBase.Get(id).ItemRarity);
                    }
                    return itemRarityList.ToArray();
                }

                float[] ReturnAdjustWeight()
                {
                    var bias = Mathf.Clamp(lootValueBias, -1f, 1f);

                    // -1〜1 を 0〜1 の目標レアリティに変換
                    var targetRarity = (bias + 1f) * 0.5f;

                    var adjustedWeights = new float[weights.Length];
                    var maxRarity = (float)ItemRarity.Legendary;

                    for (var i = 0; i < weights.Length; i++)
                    {
                        // レアリティを 0〜1 に正規化
                        var rarity = (float)itemRarities[i] / maxRarity;

                        // 目標レアリティとの差が小さいほど高い値になる
                        var rarityWeight =
                            1f - Mathf.Abs(rarity - targetRarity);

                        // 0〜1 の範囲に制限
                        rarityWeight = Mathf.Clamp01(rarityWeight);

                        // 元の重みに補正係数を掛ける
                        adjustedWeights[i] =
                            weights[i] * rarityWeight;
                    }

                    return adjustedWeights;
                }

            }
        }
    }
}