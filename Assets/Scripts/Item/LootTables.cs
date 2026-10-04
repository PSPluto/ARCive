using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "new LootTable", menuName = "LootTable", order = 0)]
    public class LootTables : ScriptableObject
    {
        /// <summary>
        /// string : ID
        /// int : 選ばれる確率の重み
        /// </summary>
        [SerializeField] private LootData[]  _lootTable;

        public LootData[] LootTable => _lootTable;
    }

    public struct LootData
    {
        public string ID;
        public int LootWeight;
    }
}