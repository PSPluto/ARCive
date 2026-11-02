using System;
using System.Collections.Generic;
using System.Linq;
using Item.Instance;
using Unity.VisualScripting;

namespace Inventory
{
    /// <summary>
    /// インベントリの根幹の動作を管理（整合性管理、アイテム移動、アイテム追加、削除など）
    /// </summary>
    public class Inventory
    {
        private ItemInstance[] items;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="size">インベントリのサイズ</param>
        public Inventory(int size)
        {
            items =  new ItemInstance[size];
        }
        
        /// <summary>
        /// アイテムを指定のスロットに追加する。
        /// </summary>
        /// <param name="item">追加するもの</param>
        /// <param name="index">追加する場所</param>
        public void AddItem(ItemInstance item, int index)
        {
            // 初期化されているかチェック
            if (items == null)
            {
                throw new NullReferenceException("items が初期化されていませんでした");
            }
            
            // 不正な値でないかチェック
            if (!GameUtility.IsBetweenInclusive(index, 0, items.GetLength(0) - 1))
            {
                throw new IndexOutOfRangeException("アイテム追加 で指定された値がインベントリのIndex範囲外でした");
            }
                
            // 実行
            switch (items[index])
            {
                case null:
                    items[index] = item;
                    break;
                
            }
        }

        /// <summary>
        /// アイテムのスタック数を減らす。
        /// </summary>
        /// <param name="index">対象のindex</param>
        /// <param name="count">減らす量</param>
        public void RemoveItem(int index ,int count)
        {
            // 初期化されているかチェック
            if (items == null)
            {
                throw new NullReferenceException("items が初期化されていませんでした");
            }            
            // インデックス内かチェック
            if (!GameUtility.IsBetweenInclusive(index, 0, items.GetLength(0) - 1))
            {
                throw new IndexOutOfRangeException("アイテム削除 で指定された値がインベントリのIndex範囲外でした");
            }
            // TODO: アイテムの数を減らす処理
        }
        
        /// <summary>
        /// アイテムのスタック数を減らす。
        /// </summary>
        /// <param name="itemId">対象のアイテムのID</param>
        /// <param name="count">減らす量</param>
        public void RemoveItem(string itemId,int count)
        {
            int itemIndex = Array.FindIndex(items ,n => n != null && n.ID == itemId);
            if (itemIndex == -1)
            {
                throw new KeyNotFoundException($"指定されたキー: {itemId} が見つかりませんでした");
            }
            RemoveItem(itemIndex, count);
        }
    }
}