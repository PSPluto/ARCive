using System.Collections.Generic;
using Item.Instance;

namespace Inventory
{
    public class InventoryOperator
    {
        
        // 変更データのリスト
        private List<ChangeInventoryData> _inventoryChangeList = new List<ChangeInventoryData>();
        private List<ChangeItemStackData> _itemStackChangeList = new List<ChangeItemStackData>();
        /// <summary>
        /// アイテムをまとめる。余った分は元の場所に戻す
        /// </summary>
        public void StackItems( ItemInstance[] fromInventory, int fromIndex,  ItemInstance[] toInventory, int toIndex)
        {
            // キャッシュ
            var fromInstance = fromInventory[fromIndex];
            var toInstance = toInventory[toIndex];

            // 計算
            var fromStack = fromInstance.StackCount;
            var toStack = toInstance.StackCount + fromInstance.StackCount;
            var remaining = toStack - toInstance.Definition.MaxStackSize;
            if (remaining > 0)
            {
                toStack = (toInstance.Definition.MaxStackSize);
                fromStack = remaining;
            }
            else
            {
                fromStack = 0;
            }
            
            // Commit処理
            _itemStackChangeList.Add(new ChangeItemStackData(toInventory, toIndex, toStack));
            if (fromStack <= 0)
            {
                _inventoryChangeList.Add(new ChangeInventoryData(fromInventory, fromIndex, null));
            }
            else
            {
                _itemStackChangeList.Add(new ChangeItemStackData(fromInventory, fromIndex, fromStack));
            }
        }

        /// <summary>
        /// アイテムを何もせず入れ替える
        /// </summary>
        public void SwapItems(ItemInstance[] fromInventory, int fromIndex, ItemInstance[] toInventory, int toIndex)
        {
            _inventoryChangeList.Add(new ChangeInventoryData(fromInventory, fromIndex, toInventory[toIndex]));
            _inventoryChangeList.Add(new ChangeInventoryData(toInventory, toIndex, fromInventory[fromIndex]));
        }
        
        public bool Commit()
        {
            //TODO:変更を確定する。
            foreach (var item in _itemStackChangeList)
            {
                item.targetInventory[item.targetIndex].ChangeStackCount(item.afterStackSize);
            }
            foreach (var changeData in _inventoryChangeList)
            {
                changeData.targetInventory[changeData.targetIndex] = changeData.afterItemInstance;
            }
            
            _itemStackChangeList.Clear();
            _inventoryChangeList.Clear();
            return true;
        }
    }
}