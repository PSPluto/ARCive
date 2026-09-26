using System;
using UnityEngine;

public class BaseCrate : MonoBehaviour , IInteraction , IPlayerStateChangeRequester
{ 
    public StateChangeRelay stateChangeRelay { get; private set; }
    
    // リクエストするステート
    [SerializeField] private PlayerState requestStateRef;
    public PlayerState requestState { get => requestStateRef;  set => requestStateRef = value; }

    public void Interact()
    {
        Debug.Log("コンテナがインタラクトされた");
        // State変更の要求
        RequestStateChange(requestState);
        
        // TODO:メインの処理


    }

    private void RequestStateChange(PlayerState newState)
    {
        stateChangeRelay.RequestStateChange(newState);
    }

    /// <summary>
    /// StateChangeRelay の参照を入れるメソッド。Playerインタラクトから呼ばれる想定
    /// </summary>
    /// <param name="newStateChangeRelay"></param>
    public void SetStateChangeRelay(StateChangeRelay newStateChangeRelay)
    {
        stateChangeRelay =  newStateChangeRelay;
    }
    
}
