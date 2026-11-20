using System;
using DefaultNamespace;
using Inventory;
using Item.Factory.lootTable;
using Item.Instance;
using UnityEngine;

public class BaseCrate : MonoBehaviour , IInteraction , IPlayerStateChangeRequester , IInventoryUIProvider
{ 
    public StateChangeRelay stateChangeRelay { get; private set; }
    public Item.Instance.ItemInstance[] crateInventory = new Item.Instance.ItemInstance[4];
    [SerializeField] private LootSettings lootSettings;
    [SerializeField] private bool useLootValueBiasOverride;
    [SerializeField, Range(-1f,1f)] private float lootValueBiasOverride;
    
    // リクエストするステート
    [SerializeField] private PlayerState requestStateRef;
    public PlayerState requestState { get => requestStateRef;  set => requestStateRef = value; }

    public void Interact(PlayerInventory playerInventory)
    {
        Debug.Log("コンテナがインタラクトされた");
        // State変更の要求
        RequestStateChange(requestState);
        
        //アイテムを生成
        for(int i = 0; i < crateInventory.Length; i++)
        {
            // null合体演算子：?? 左辺がnullの場合は右辺の値を使うやつ(追記:使うのやめた。)
            crateInventory[i] = Item.Factory.ItemFactory.CreateItemInstance(lootSettings.lootTable,useLootValueBiasOverride? lootValueBiasOverride :lootSettings.lootDefaultBias);
        }
        

    }

    [ContextMenu("箱の中身を確認")]
    public void CheckInInventory()
    {
        foreach (var slot in crateInventory)
        {
            if (slot == null)
            {
                Debug.Log($"slot:{slot} , null");
                continue;
            }
            Debug.Log($"slot:{slot} , ID:{slot.ID} , Stack:{slot.StackCount} , Def:{slot.Definition}");
        }
    }

    private void RequestStateChange(PlayerState newState)
    {
        stateChangeRelay.RequestStateChange(newState);
    }

    /// <summary>
    /// StateChangeRelay の参照を入れるメソッド。Playerインタラクトから呼ばれる想定
    /// </summary>
    /// <param name="newStateChangeRelay"></param>
    public void SetStateChangeRelay(StateChangeRelay newStateChangeRelay)
    {
        stateChangeRelay =  newStateChangeRelay;
    }

    public ItemInstance[] GetAllInventoryItems()
    {
        return crateInventory;
    }

    public bool SetItem(ItemInstance item, int index)
    {
        crateInventory[index] = item;
        return true;
    }
}
