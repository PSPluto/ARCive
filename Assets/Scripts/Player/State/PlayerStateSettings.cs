using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStateSettings", menuName = "Scriptable Objects/Player State Settings")]
public class PlayerStateSettings : ScriptableObject
{
    public bool canMove;
    public bool canFire;
    public bool canReload;
    public bool canInteract;
    public bool canUpdateAim;
}
