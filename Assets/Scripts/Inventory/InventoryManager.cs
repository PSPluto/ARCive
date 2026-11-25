using System;
using System.Collections.Generic;
using Item.Instance;
using JetBrains.Annotations;
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
        private List<ChangeData> changeList =  new List<ChangeData>();
        
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
        public bool MoveItems( [NotNull] in ItemInstance[] fromInventory, int fromIndex, in ItemInstance[] toInventory, int toIndex)
        {
            // 簡易バリデーションチェック
            if (fromInventory == null || toInventory == null) throw new ArgumentNullException(nameof(fromInventory));
            if ((!IsInRange(fromIndex, 0, fromInventory.Length-1) || !IsInRange(toIndex, 0, toInventory.Length-1)))
            {
                throw new IndexOutOfRangeException("指定インデックスがインベントリ範囲外です");
            }
            
            // どの処理を行うことになるか判定
            if (fromInventory[fromIndex].ID != toInventory[toIndex].ID)
            {
                // IDが違うので、交換
                SwapItems(fromInventory, fromIndex, toInventory, toIndex);
            }
            else if (toInventory[toIndex].StackCount < toInventory[toIndex].Definition.MaxStackSize)
            {
                // スタック数が最大スタック数よりも小さい 処理
                StackItems(fromInventory, fromIndex, toInventory, toIndex);
            }
            //TODO: 他の処理

            Commit();
            return false;
        }

        /// <summary>
        /// アイテムをまとめる。余った分は元の場所に戻す
        /// </summary>
        private void StackItems(ItemInstance[] fromInventoryCopy, int copyFromIndex, ItemInstance[] toInventoryCopy, int copyToIndex)
        {
            var fromStack =  fromInventoryCopy[copyFromIndex].StackCount;
            var toStack =  toInventoryCopy[copyToIndex].StackCount;
            var toMaxStack = toInventoryCopy[copyToIndex].Definition.MaxStackSize;

            toStack += fromStack;
            var remaining =  toStack - toMaxStack;
            if (remaining > 0)
            {
                // 1以上多い場合、
                fromInventoryCopy[copyFromIndex].ChangeStackCount(remaining);
                changeList.Add(new ChangeData(fromInventoryCopy, copyFromIndex, toInventoryCopy[copyToIndex]));
                
            }
            else
            {
                changeList.Add(new ChangeData(fromInventoryCopy, copyFromIndex, null));
            }
            changeList.Add(new ChangeData(toInventoryCopy, copyToIndex, fromInventoryCopy[copyFromIndex]));
        }

        /// <summary>
        /// アイテムを何もせず入れ替える
        /// </summary>
        private void SwapItems(ItemInstance[] fromInventory, int fromIndex, ItemInstance[] toInventory, int toIndex)
        {
            changeList.Add(new ChangeData(fromInventory, fromIndex, toInventory[toIndex]));
            changeList.Add(new ChangeData(toInventory, toIndex, fromInventory[fromIndex]));
        }
        
        //TODO:トランザクション処理関係は後で別クラスに分ける。SwapItems()なども含めて。

        private bool Commit()
        {
            //TODO:変更を確定する。
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
        public ChangeData(ItemInstance[] inventory, int index, ItemInstance itemInstance)
        {
            targetInventory = inventory;
            targetIndex = index;
            newItemInstance = itemInstance;
        }
    }
}