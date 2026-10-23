using UnityEngine;

namespace Item.Database
{
    public class ItemDatabaseManager : MonoBehaviour
    {
        public static ItemDatabaseManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        [SerializeField] private ItemDatabase itemDatabase;

        public ItemDatabase Database => itemDatabase;
    }
}