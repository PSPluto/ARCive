using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    public void Fire()
    {
        weapon.Fire();
    }
}
