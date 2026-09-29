using UnityEngine;
/// <summary>
/// アイテムの元データ_イミュータブル。
/// </summary>
public abstract class ItemData : ScriptableObject
{
    [SerializeField] private string itemID;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    
    
    // インスペクターでの編集と別クラスからの書き換え防止を同時に行うため
    public string ItemID => itemID;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    
}
