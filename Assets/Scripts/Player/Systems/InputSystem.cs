using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private MainInputAction playerControls;
    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool RunInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool ChangeItemInput { get; private set; }
    public float ChangeItemValue { get; private set; }
    public bool PauseInput { get; private set; }

    public void Start()
    {
        playerControls = new MainInputAction();
        playerControls.Player.Enable();
    }
    
    public void Disable()
    {
        playerControls.Player.Disable();
    }

    public void Update()
    {
        MoveInput = playerControls.Player.Move.ReadValue<Vector2>();
        JumpInput = playerControls.Player.Jump.WasPerformedThisFrame();
        RunInput = playerControls.Player.Run.IsPressed();
        AttackInput = playerControls.Player.Attack.WasPerformedThisFrame();
        PauseInput = playerControls.Player.Pause.WasPerformedThisFrame();

        if (playerControls.Player.ChangeItem.WasPerformedThisFrame())
        {
            ChangeItemValue = playerControls.Player.ChangeItem.ReadValue<float>();
        }
        else
        {
            ChangeItemValue = 0f;
        }
    }
}
