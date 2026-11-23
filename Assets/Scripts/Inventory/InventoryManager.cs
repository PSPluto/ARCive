using System;
using Item.Instance;
using Unity.VisualScripting;
using UnityEngine;
using static GameUtility;

namespace Inventory
{
    // TODO: 最終的にはモノビヘイビアによるインスタンス化をやめる
    public class InventoryManager :  MonoBehaviour
    {
        // TODO:最終的にはこれをUIに持ってもらう。今はUIがないのでここにおいておく
        public ItemInstance[] PlayerInventory { get; private set; }
        public ItemInstance[] OpenedInventory { get; private set; }

        public static InventoryManager Instance;

        private void Awake()
        {
            if(Instance)return;
            
            Instance = this;
        }

        /// <summary>
        /// プレイヤーのインベントリの参照を変更する
        /// </summary>
        /// <param name="playerInventory"></param>
        public void SetPlayerInventory(ItemInstance[] playerInventory)
        {
            PlayerInventory = playerInventory;
        }
        /// <summary>
        /// もう一方のインベントリの参照を変更する
        /// </summary>
        /// <param name="openedInventory"></param>
        public void SetOpenedInventory(ItemInstance[] openedInventory)
        {
            OpenedInventory = openedInventory;
        }
        
        /// <summary>
        /// アイテムを移動する。（ドラッグ移動想定）行先にアイテムがあれば入れ替える。行先のアイテムが同種、かつ満タンでなければ、できる限りまとめ、残りを戻す
        /// </summary>
        /// <param name="fromInventory"></param>
        /// <param name="fromIndex"></param>
        /// <param name="toInventory"></param>
        /// <param name="toIndex"></param>
        /// <returns></returns>
        public bool MoveItems( ref ItemInstance[] fromInventory, int fromIndex, ref ItemInstance[] toInventory, int toIndex)
        {
            var from= fromInventory[fromIndex];
            var to= toInventory[toIndex];

            var tmpFrom = from.Clone();
            var tmpTo = to.Clone();
            
            // 不正値チェック
            if (IsInRange(fromIndex, 0, fromInventory.Length-1) || IsInRange(toIndex, 0, toInventory.Length-1))
            {
                throw new IndexOutOfRangeException("指定インデックスがインベントリ範囲外です");
                return false;
            }
            
            // どの処理を行うことになるか判定
            if (tmpFrom.ID != tmpTo.ID)
            {
                // IDが違うので、交換
                SwapItems(tmpFrom, tmpTo);
            }
            //TODO: 他の処理
            return false;
        }

        /// <summary>
        /// 2つのアイテムを入れ替える
        /// </summary>
        /// <param name="tmpFrom"></param>
        /// <param name="tmpTo"></param>
        /// <returns>  </returns>
        private (ItemInstance From, ItemInstance To) SwapItems(ItemInstance tmpFrom, ItemInstance tmpTo)
        {
            // // Pythonみたいに値入れ替えができる。...けど今回使わなくていいかも
            // (tmpTo, tmpFrom) = (tmpFrom, tmpTo);

            // 値を入れ替えて返す
            return (From:tmpTo, To:tmpFrom);
        }

        private bool Commit()
        {
            
        }
    }
}