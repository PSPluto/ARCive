using System;
using System.Collections.Generic;
using Item.Instance;

namespace Item.Factory
{ 
    public class ItemInstanceFactoryDatabase
    {
        private readonly Dictionary<string, Func<ItemInstance>> _instanceFactoryDatabase = new Dictionary<string, Func<ItemInstance>>()
            {
                ["TmpItem"] = () => new ItemInstance("TmpItem", 3),
                ["TmpItem2"] = () => new DurableInstance("TmpItem2", 20),
                // Key:ID、Value:new 式
                //TODO: Itemをハードコード
            };

        public bool TryGetValue(string key, out Func<ItemInstance> resultFunc)
        {
            var result = _instanceFactoryDatabase.TryGetValue(key, out Func<ItemInstance> value);
            resultFunc = value;
            return result;
            
        }

        
        

    }
}