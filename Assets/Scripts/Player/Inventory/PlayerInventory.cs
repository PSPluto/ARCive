using Inventory;
using Item.Instance;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerInventory :  MonoBehaviour,IInventoryUIProvider
    {
        // TODO: インターフェースで「これはインベントリシステムと互換がある」仕組みを実装する。
        private Item.Instance.ItemInstance[] _playerInventory = new Item.Instance.ItemInstance[24];

        public ItemInstance[] GetAllInventoryItems()
        {
            return _playerInventory;
        }

        public bool SetItem(ItemInstance item, int index)
        {
            _playerInventory[index] = item;
            return true;
        }
    }
}