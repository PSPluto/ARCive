using System;
using System.Collections.Generic;
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

        // 仮でstatic化
        public static InventoryManager Instance;

        // 変更データのリスト
        List<ChangeData> changeList =  new List<ChangeData>();
        
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
        public bool MoveItems( in ItemInstance[] fromInventory, int fromIndex, in ItemInstance[] toInventory, int toIndex)
        {
            // まずは不正値チェック
            if (IsInRange(fromIndex, 0, fromInventory.Length-1) || IsInRange(toIndex, 0, toInventory.Length-1))
            {
                throw new IndexOutOfRangeException("指定インデックスがインベントリ範囲外です");
                return false;
            }
            

            
            // どの処理を行うことになるか判定
            if (fromInventory[fromIndex].ID != toInventory[toIndex].ID)
            {
                // IDが違うので、交換
                SwapItems();
            }
            //TODO: 他の処理
            return false;
        }
        
        private void SwapItems()
        {
            changeList.Add(new ChangeData());
        }

        private bool Commit()
        {
            //変更を確定する。
            return true;
        }
    }

    /// <summary>
    /// 変更データの構造体(クラスでもいいのかな)
    /// </summary>
    public struct ChangeData
    {
        public ItemInstance[] targetInventory;
        public int targetIndex;
        public ItemInstance newItemInstance;

        /// <summary>
        /// 変更データのコンストラクタ
        /// </summary>
        /// <param name="inventory">対象のインベントリ</param>
        /// <param name="index">対象のIndex</param>
        /// <param name="itemInstance">このアイテムで置き換える</param>
        ChangeData(ItemInstance[] inventory, int index, ItemInstance itemInstance)
        {
            targetInventory = inventory;
            targetIndex = index;
            newItemInstance = itemInstance;
        }
    }
}