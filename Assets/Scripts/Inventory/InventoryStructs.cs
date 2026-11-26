using Item.Instance;

namespace Inventory
{
    /// <summary>
    /// 変更データの構造体(クラスでもいいのかな)
    /// </summary>
    public struct ChangeInventoryData
    {
        public ItemInstance[] targetInventory;
        public int targetIndex;
        public ItemInstance afterItemInstance;

        /// <summary>
        /// 変更データのコンストラクタ
        /// </summary>
        /// <param name="inventory">対象のインベントリ</param>
        /// <param name="index">対象のIndex</param>
        /// <param name="itemInstance">このアイテムで置き換える</param>
        public ChangeInventoryData(ItemInstance[] inventory, int index, ItemInstance itemInstance)
        {
            targetInventory = inventory;
            targetIndex = index;
            afterItemInstance = itemInstance;
        }
    }
    public struct ChangeItemStackData
    {
        public ItemInstance[] targetInventory;
        public int targetIndex;
        public int afterStackSize;

        public ChangeItemStackData(ItemInstance[] inventory, int index, int stackSize)
        {
            targetInventory = inventory;
            targetIndex = index;
            afterStackSize = stackSize;
        }
    }
}