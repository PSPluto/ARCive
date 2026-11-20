using Item.Database.ItemDataDerived;

namespace Item.Instance
{
    public class DurableInstance : ItemInstance
    {
        private readonly DurableData durableData;

        public int CurrentDurability { get; private set; }

        public void ChangeDurabilityValue(int value)
        {
            CurrentDurability = UnityEngine.Mathf.Clamp(value, 0, durableData.MaxDurability);
        }
        
        public DurableInstance(DurableData definition, int currentDurability) : base(definition, 1)
        {
            durableData = definition;
            CurrentDurability = UnityEngine.Mathf.Clamp(currentDurability, 1, durableData.MaxDurability);
        }
    }
}