namespace Item.Instance
{
    public class DurableInstance
    {
        public int CurrentDurability { get; private set; }

        public void SetDurabilityValue(int value)
        {
            CurrentDurability = value;
        }
    }
}