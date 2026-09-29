using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class WeaponData : ItemData
{
    [SerializeField] private int bulletDamage;
    [SerializeField] private int magazineSize;
    
    public int Damage => bulletDamage;
    public int MaxDurability => magazineSize;
}
