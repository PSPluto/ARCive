using UnityEngine;

namespace Item.Factory.lootTable
{
    [CreateAssetMenu(fileName = "new LootSettingsAsset", menuName = "LootSettingsAsset", order = 0)]
    public class LootSettings : ScriptableObject
    {
        [Header("// General //")]
        [Tooltip("箱が持つスロットの数")] public int inventorySize = 6;
        
        [Header("// LootSettings //")]
        [Tooltip("この箱のルートテーブル")]public LootTable lootTable;
        [Tooltip("この箱の基本バイアス"),Range(-1f,1f)]public float lootDefaultBias = 0;
    }
}