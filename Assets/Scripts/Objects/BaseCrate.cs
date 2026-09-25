using System;
using UnityEngine;

public class BaseCrate : MonoBehaviour , IInteraction
{
    public PlayerState? RequestState { get; private set; } = PlayerState.Interacting;

    public void Interact()
    {
        Debug.Log("Interact");
    }
}
