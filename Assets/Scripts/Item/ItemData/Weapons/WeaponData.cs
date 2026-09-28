using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public abstract class WeaponData : ItemData
{
    [SerializeField] private int damage;
    [SerializeField] private int maxDurability;
    
    public int Damage => damage;
    public int MaxDurability => maxDurability;
}
