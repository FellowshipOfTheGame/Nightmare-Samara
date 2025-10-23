using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem 
{
    private MainInputAction playerControls;
    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool RunInput { get; private set; }
    public bool AttackInput { get; private set; }
    public float ChangeItemInput { get; private set; }
    public bool PauseInput { get; private set; }

    public InputSystem()
    {
        playerControls = new MainInputAction();
    }

    public void Enable()
    {
        playerControls.Player.Enable();
    }

    public void Disable()
    {
        playerControls.Player.Disable();
    }

    public void Tick()
    {
        MoveInput = playerControls.Player.Move.ReadValue<Vector2>();
        JumpInput = playerControls.Player.Jump.WasPerformedThisFrame();
        RunInput = playerControls.Player.Run.IsPressed();
        AttackInput = playerControls.Player.Attack.WasPerformedThisFrame();
        ChangeItemInput = playerControls.Player.ChangeItem.ReadValue<float>();
        PauseInput = playerControls.Player.Pause.WasPerformedThisFrame();
    }
}
