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
        // == 参照
        private readonly InventoryOperator _inventoryOperator =  new InventoryOperator();
        
        // TODO:最終的にはこれをUIに持ってもらう。今はUIがないのでここにおいておく
        public ItemInstance[] PlayerInventory { get; private set; }
        public ItemInstance[] OpenedInventory { get; private set; }

        // 仮でシングルトン化
        public static InventoryManager Instance;
        
        private void Awake()
        {
            if(Instance)return;
            
            Instance = this;
        }
        
        
        /// <summary>
        /// アイテムを移動する。（ドラッグ移動想定）行先にアイテムがあれば入れ替える。行先のアイテムが同種、かつ満タンでなければ、できる限りまとめ、残りを戻す
        /// </summary>
        /// <param name="fromInventory"></param>
        /// <param name="fromIndex"></param>
        /// <param name="toInventory"></param>
        /// <param name="toIndex"></param>
        /// <returns></returns>
        public void MoveItems( in ItemInstance[] fromInventory, int fromIndex, in ItemInstance[] toInventory, int toIndex)
        {
            // バリデーションチェック
            if (!CheckValidation(fromInventory, fromIndex, toInventory, toIndex)) { return; }
            // 元の位置と先の位置が同じだった場合、なんもないので早期return
            if (fromInventory == toInventory && fromIndex == toIndex) { return; }
            
            // どの処理を行うことになるか判定 
            Action action = DetermineOperation(fromInventory,fromIndex,toInventory,toIndex);
            // 実行
            action?.Invoke();
            _inventoryOperator.Commit();
        }

        private bool CheckValidation(ItemInstance[] fromInventory, int fromIndex, ItemInstance[] toInventory, int toIndex)
        {
            if (fromInventory == null) throw new ArgumentNullException(nameof(fromInventory));
            if (toInventory == null) throw new ArgumentNullException(nameof(toInventory));
            if ((!IsInRange(fromIndex, 0, fromInventory.Length-1) || !IsInRange(toIndex, 0, toInventory.Length-1)))
            {
                throw new IndexOutOfRangeException("指定インデックスがインベントリ範囲外です");
            }
            if (fromInventory[fromIndex] == null)
            {
                throw new ArgumentException("指定インデックスの値が不正です");
            }
            return true;
        }

        private Action DetermineOperation( ItemInstance[] fromInventory, int fromIndex,  ItemInstance[] toInventory, int toIndex)
        {
            if (toInventory[toIndex] == null || fromInventory[fromIndex].ID != toInventory[toIndex].ID)
            {
                // IDが違った場合(交換処理)
                return () => _inventoryOperator.SwapItems(fromInventory, fromIndex, toInventory, toIndex);
            }
            else if (toInventory[toIndex].StackCount < toInventory[toIndex].Definition.MaxStackSize)
            {
                // ID同じ、かつ行先のスタック数に余裕があった場合(スタック処理)
                return () => _inventoryOperator.StackItems(fromInventory, fromIndex, toInventory, toIndex);
            }
            // 実行できる操作がなかった。
            return null;
        }
        
        // インベントリの配列をSetする用メソッドたち
        public void SetPlayerInventory(ItemInstance[] playerInventory)
        {
            PlayerInventory = playerInventory;
        }
        public void SetOpenedInventory(ItemInstance[] openedInventory)
        {
            OpenedInventory = openedInventory;
        }
    }
}