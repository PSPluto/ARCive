using UnityEngine;

public class StateSettingsDatabase : MonoBehaviour
{
    [SerializeField] private PlayerStateSettings[] settings;

    public PlayerStateSettings Get(PlayerState state)
    {
        return settings[(int)state];
    }
}
