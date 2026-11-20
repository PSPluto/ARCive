using Inventory;
using Item.Instance;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerInventory :  MonoBehaviour,IInventoryUIProvider
    {
        // TODO: インターフェースで「これはインベントリシステムと互換がある」仕組みを実装する。
        private Item.Instance.ItemInstance[] _playerInventory = new Item.Instance.ItemInstance[24];

        /// <summary>
        /// 自分のインベントリを配列で取得する
        /// </summary>
        /// <returns></returns>
        public ItemInstance[] GetAllInventoryItems()
        {
            return _playerInventory;
        }

        /// <summary>
        /// 特定のスロットにアイテムをセットする
        /// </summary>
        /// <param name="item">アイテムインスタンス</param>
        /// <param name="index">場所</param>
        /// <returns>成功したかどうか</returns>
        public bool SetItem(ItemInstance item, int index)
        {
            _playerInventory[index] = item;
            return true;
        }
    }
}