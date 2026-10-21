using System;
using System.Collections.Generic;
using Item.Database;
using Item.Instance;

namespace Item.Factory
{ 
    public class ItemInstanceFactoryDatabase
    {
        private readonly Dictionary<string, Func<ItemInstance>> _instanceFactoryDatabase;

        public ItemInstanceFactoryDatabase(ItemDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            _instanceFactoryDatabase = new Dictionary<string, Func<ItemInstance>>();

            if (database.TryGet("TmpItem", out var itemData))
            {
                _instanceFactoryDatabase[itemData.ID] = () => new ItemInstance(itemData, 3);
            }

            if (database.TryGet("TmpItem2", out var durableData) &&
                durableData is Item.Database.ItemDataDerived.DurableData durableItemData)
            {
                _instanceFactoryDatabase[durableData.ID] =
                    () => new DurableInstance(durableItemData, 20);
            }
        }

        public bool TryGetValue(string key, out Func<ItemInstance> resultFunc)
        {
            var result = _instanceFactoryDatabase.TryGetValue(key, out Func<ItemInstance> value);
            resultFunc = value;
            return result;
            
        }

        
        

    }
}