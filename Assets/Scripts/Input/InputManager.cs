using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    private GameInputActions input;
    public bool CanMove { get; set; } = true;
    public bool CanFire { get; set; } = true;
    public bool CanReload { get; set; } = true;
    public bool CanInteract { get; set; } = true;
    public bool CanAim { get; set; } = true;
    public bool CanDash { get; set; } = true;
    
    public bool CanCloseUi { get; set; } = true;
    
    
    
    private void Awake()
    {
        Instance = this;

        input = new GameInputActions();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    
    // プレイヤー

    public void AllPlayerInputDisabled()
    {
        CanMove = false;
        CanFire = false;
        CanReload = false;
        CanInteract = false;
        CanAim = false;
        CanDash = false;
    }
    public void AllPlayerInputEnabled()
    {
        CanMove = true;
        CanFire = true;
        CanReload = true;
        CanInteract = true;
        CanAim = true;
        CanDash = true;
    }
    
    public Vector2 Move => CanMove ?input.Player.Move.ReadValue<Vector2>() : Vector2.zero;
    public bool Fire => CanFire && input.Player.Fire.IsPressed();
    public bool Aim => CanAim && input.Player.Aim.IsPressed();
    public bool Reload => CanReload && input.Player.Reload.WasPressedThisFrame();
    public bool Interact => CanInteract && input.Player.Interact.WasPressedThisFrame();
    public bool Dash => CanDash && input.Player.Dash.IsPressed();
    
    // UI
    public bool CloseUi => CanCloseUi && input.Player.UIClose.WasPressedThisFrame();
        
    
    
    // 制御
    public void SetPlayerInput(bool enabled)
    {
        if (enabled)
            input.Player.Enable();
        else
            input.Player.Disable();
    }

    public void SetUIInput(bool enabled)
    {
        if (enabled)
            input.UI.Enable();
        else
            input.UI.Disable();
    }
}