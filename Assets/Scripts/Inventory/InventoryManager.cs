using System;
using UnityEngine;

namespace Inventory
{
    // TODO: 最終的にはモノビヘイビアによるインスタンス化をやめる
    public class InventoryManager :  MonoBehaviour
    {
        // TODO: 
        // 以下の操作機能を作成する
        // ・どの操作を行うかの判定処理⇒
        // ・交換
        // ・移動
        // ・スタック（と返却）
        // ・クイック移動
        // ーーー
        // ・分割
        // ・ドロップ

        public static InventoryManager Instance;

        private void Awake()
        {
            if(Instance)return;
            
            Instance = this;
        }
        
        
        
    }
}