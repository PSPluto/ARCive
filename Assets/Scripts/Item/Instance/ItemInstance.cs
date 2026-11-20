using System;
using Item.Database;
using UnityEngine;

namespace Item.Instance
{
    /// <summary>
    /// アイテムの実体データ。主に状態を持つ
    /// </summary>
    public class ItemInstance
    {
        // アイテムの固定データ
        public ItemData Definition { get; }
        public string ID => Definition.ID;
        public int StackCount { get; private set; }

        public ItemInstance(ItemData definition, int stackCount)
        {
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));

            Definition = definition;
            StackCount = Mathf.Clamp(
                stackCount,
                1,
                definition.MaxStackSize);
        }

        /// <summary>
        /// スタック数をセットします
        /// </summary>
        public void ChangeStackCount(int value)
        {
            StackCount = Math.Min(value , Definition.MaxStackSize);
        }
    }
}