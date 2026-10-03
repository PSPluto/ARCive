namespace Item.Instance
{
    // Stackableのインスタンスは、現在のスタック量 以外の状態を持てない。
    public sealed class StackableInstance
    {
        public int CurrentStack {get; private set;}

        public void SetStackCount(int count)
        {
            CurrentStack = count;
        }
    }
}