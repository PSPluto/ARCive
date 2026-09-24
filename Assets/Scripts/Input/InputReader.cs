using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private GameInputActions _input;

    // --- 初期化 --- //
    private void Awake()
    {
        _input = new GameInputActions();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    
    
    // --- メイン --- //

    public Vector2 Move => _input.Player.Move.ReadValue<Vector2>();
    public bool Fire => _input.Player.Fire.WasPressedThisFrame();
}
