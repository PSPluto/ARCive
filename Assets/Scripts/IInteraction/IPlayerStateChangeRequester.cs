public interface IPlayerStateChangeRequester
{
    StateChangeRelay stateChangeRelay { get; }
    PlayerState requestState { get; set; }

    public void SetStateChangeRelay(StateChangeRelay newStateChangeRelay);
    // stateChangeRelay =  substituteStateChangeRelay;

    public void RequestStateChange(PlayerState newState)
    {
        stateChangeRelay.RequestStateChange(newState);
    }
    
    
}
