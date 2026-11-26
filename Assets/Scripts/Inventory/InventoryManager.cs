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
        private List<ChangeInventoryData> _inventoryChangeList = new List<ChangeInventoryData>();
        private List<ChangeItemStackData> _itemStackChangeList = new List<ChangeItemStackData>();
        
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
            // 簡易バリデーションチェック
            if (fromInventory == null) throw new ArgumentNullException(nameof(fromInventory));
            if (toInventory == null) throw new ArgumentNullException(nameof(toInventory));
            if ((!IsInRange(fromIndex, 0, fromInventory.Length-1) || !IsInRange(toIndex, 0, toInventory.Length-1)))
            {
                throw new IndexOutOfRangeException("指定インデックスがインベントリ範囲外です");
            }
            if (fromInventory[fromIndex] == null || toInventory[toIndex] == null)
            {
                throw new ArgumentException("指定インデックスの値が不正です");
            }
            
            // どの処理を行うことになるか判定
            if (fromInventory[fromIndex].ID != toInventory[toIndex].ID)
            {
                // IDが違うので交換
                SwapItems(fromInventory, fromIndex, toInventory, toIndex);
            }
            else if (toInventory[toIndex].StackCount < toInventory[toIndex].Definition.MaxStackSize)
            {
                // スタック数が最大スタック数よりも小さい 
                StackItems(fromInventory, fromIndex, toInventory, toIndex);
            }
            //TODO: 他の処理

            Commit();
            return false;
        }

        /// <summary>
        /// アイテムをまとめる。余った分は元の場所に戻す
        /// </summary>
        private void StackItems(in ItemInstance[] fromInventory, int fromIndex, in ItemInstance[] toInventory, int toIndex)
        {
            // コードを長くしないために
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
        private void SwapItems(in ItemInstance[] fromInventory, int fromIndex, in ItemInstance[] toInventory, int toIndex)
        {
            _inventoryChangeList.Add(new ChangeInventoryData(fromInventory, fromIndex, toInventory[toIndex]));
            _inventoryChangeList.Add(new ChangeInventoryData(toInventory, toIndex, fromInventory[fromIndex]));
        }
        
        //TODO:トランザクション処理関係は後で別クラスに分ける。SwapItems()なども含めて。

        private bool Commit()
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