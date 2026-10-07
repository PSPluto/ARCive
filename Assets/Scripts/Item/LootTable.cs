using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "NewLootTable", menuName = "Item/Loot Table", order = 0)]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private LootEntry[] _entries;

        public IReadOnlyList<LootEntry> Entries => _entries;
    }

    [System.Serializable]
    public struct LootEntry
    {
        public string id;
        public float weight;
    }
}