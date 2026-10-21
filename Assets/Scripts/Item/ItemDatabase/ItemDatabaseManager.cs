using UnityEngine;
namespace Item.Database
{
    public class ItemDatabaseManager : MonoBehaviour
    {
        public static ItemDatabaseManager Instance = null;

        private void Awake()
        {
            if (Instance != null) return; 
            Instance = this;
        }

        [SerializeField] private ItemDatabase itemDatabase;

        public ItemDatabase Database => itemDatabase;
    }
}