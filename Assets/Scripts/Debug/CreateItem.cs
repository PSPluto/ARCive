using Item;
using Item.Factory;
using Item.Factory.lootTable;
using UnityEngine;
using Item.Instance;

namespace ArcDebug
{
    public class ItemFactoryTest : MonoBehaviour
    {
        [SerializeField] private LootTable lootTable;
        [SerializeField, Range(-1f, 1f)]
        private float lootValueBias;

        [ContextMenu("アイテム生成を実行 x1")]
        private void CreateItem()
        {
            ItemInstance instance = Item.Factory.ItemFactory.CreateItemInstance(lootTable, lootValueBias);

            if (instance == null)
            {
                Debug.LogError("アイテム生成に失敗");
                return;
            }

            Debug.Log($"生成成功: {instance.Definition.ItemName}, ID={instance.ID}, Stack={instance.StackCount}");

            if (instance is DurableInstance durable)
            {
                Debug.Log($"耐久値: {durable.CurrentDurability}");
            }
        }

        [ContextMenu("アイテム生成を実行 x100")]
        private void CreateItemTen()
        {
            for (int i = 0; i != 100; i++)
            {
                CreateItem();
            }
        }
    }
}
