using UnityEngine;

public enum ItemAnimationType
{
    Normal,
    BigItem,
    AR,
    SideArm,
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Scriptable Objects/ItemData")]
public class Itemdata : ScriptableObject
{
    public ItemAnimationType itemAnimType;
    public GameObject itemObject;
    
    public float maxWalkSpeed = 1.75f;
    public float maxRunSpeed = 2.75f;
}
