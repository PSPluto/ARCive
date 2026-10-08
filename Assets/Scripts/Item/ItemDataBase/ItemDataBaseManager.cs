using UnityEngine;

namespace Item.DataBase
{
    public class ItemDataBaseManager : MonoBehaviour
    {
        public static ItemDataBaseManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] private ItemDataBase itemDataBase;

        public ItemDataBase DataBase => itemDataBase;
    }
}