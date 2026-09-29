/// <summary>
/// スタック可能なアイテムのインスタンス用
/// </summary>
public class StackableItemInstance: ItemInstance
{ 
    public int CurrentStackSize;

    public StackableItemInstance(string id, int stackSize) : base(id)
    {
        CurrentStackSize = stackSize;
    }
}
