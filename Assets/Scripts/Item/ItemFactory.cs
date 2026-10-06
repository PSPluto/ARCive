using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Item.Data;

namespace Item
{
    public class ItemFactory
    {
        [SerializeField] private ItemInstanceFactoryDatabase _instanceFactoryDatabase;
        /// <summary>
        /// lootTableと戦利品価値の重み から実際にアイテムを生成してリターンします。
        /// </summary>
        /// <param name="lootTables"></param>
        /// <param name="lootValueBias"></param>
        /// <returns></returns>
        public Instance.ItemInstance CreateItemInstance(LootTable lootTable, float lootValueBias)
        {
            var hitIndex =  SearchIndex();

            // TODO: リターンでnew式を格納したラムダ式を返す。
            
            
            return null;
            
            
            
            int SearchIndex()
            {
                var weights = lootTable.Entries.Select(entry => entry.weight).ToArray();
                
                // ここに全体重みの処理を書く。
                // IDから価値を引っ張ってきて、「高価さ」によってlootValueBiasを適用する量を個々で決めていく。
                
                var ids = lootTable.Entries.Select(entry => entry.id).ToArray();
                ItemRarity[] itemRarities = ReturnItemRarities();
                
                
                
                
                // 重みの合計値
                var totalWeight = weights.Sum();
                var randomValue = Random.Range(0, totalWeight);
                
                int currentSum = 0;
                for (int i = 0; i < weights.Length; i++)
                {
                    currentSum += weights[i];

                    if (randomValue < currentSum)
                    {
                        return i;
                    }
                }

                return 0;

                ItemRarity[] ReturnItemRarities()
                {
                    List<ItemRarity> itemRarities = new List<ItemRarity>();
                    foreach (var id in ids)
                    {
                        itemRarities.Add(ItemDataBaseManager.Instance.DataBase.Get(id).ItemRarity);
                    }
                    return itemRarities.ToArray();
                }
            }
        }
    }
}