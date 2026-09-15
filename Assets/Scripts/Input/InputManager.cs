using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private GameInputActions input;

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
    
    // Player Input

    public Vector2 Move =>
        input.Player.Move.ReadValue<Vector2>();

    public bool Fire =>
        input.Player.Fire.IsPressed();

    public bool Aim =>
        input.Player.Aim.IsPressed();

    public bool Reload =>
        input.Player.Reload.WasPressedThisFrame();

    public bool Interact =>
        input.Player.Interact.WasPressedThisFrame();
    
    
    // Input Control
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