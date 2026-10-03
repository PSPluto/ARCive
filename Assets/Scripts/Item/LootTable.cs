using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "new LootTable", menuName = "LootTable", order = 0)]
    public class LootTable : ScriptableObject
    {
        /// <summary>
        /// string : ID
        /// int : 選ばれる確率の重み
        /// </summary>
        [SerializeField] private Dictionary<string, int> _lootTable = new();
        
        public string GetLoot(float lootWeight)
        {
            string resultId = 1.ToString();
            return resultId;
        }
    }
}