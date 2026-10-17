using UnityEngine;
using UnityEngine.Serialization;

namespace Item.Database
{
    public class ItemDatabaseManager : MonoBehaviour
    {
        public static ItemDatabaseManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        [FormerlySerializedAs("itemDataBase")] [SerializeField] private ItemDatabase itemDatabase;

        public ItemDatabase Database => itemDatabase;
    }
}