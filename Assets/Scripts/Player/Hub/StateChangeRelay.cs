using UnityEngine;

public class StateChangeRelay : MonoBehaviour
{
    [SerializeField] private Player player;
    public void RequestStateChange(PlayerState newState)
    {
        player.ChangeState(newState);
    }
}
