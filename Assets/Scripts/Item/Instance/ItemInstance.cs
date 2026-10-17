using System;
using Item.Database;
using UnityEngine;

namespace Item.Instance
{
    /// <summary>
    /// アイテムの実体データ。主に「状態」を持つ
    /// </summary>
    public class ItemInstance
    {
        public string ID { get; protected set; }
        public int StackCount { get; protected set; }
        
        public ItemInstance(string id, int stackCount )
        {
            ID = id;
            StackCount = Mathf.Clamp(stackCount,1,((ItemDatabaseManager.Instance.Database.Get(id))).MaxStackSize);
        }
    }
}