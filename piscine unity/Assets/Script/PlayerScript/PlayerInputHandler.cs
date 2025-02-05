using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public FrameInput FrameInput { get; private set; }
    PlayerControl playerinputActions;
    InputAction _move, _dash, _interact, _shoot,_jump;

    private void Awake()
    {
        playerinputActions = new PlayerControl();
        _move = playerinputActions.Player.Move;
        _dash = playerinputActions.Player.Jump;
        _shoot = playerinputActions.Player.Attack;
        _jump = playerinputActions.Player.Jump;
       
    }

    private void OnEnable()
    {
        playerinputActions.Enable();
    }

    private void OnDisable()
    {
        playerinputActions.Disable();
    }
    private void Update()
    {
        FrameInput = GatherInput();
    }
    FrameInput GatherInput()
    {
        return new FrameInput
        {
            Move = _move.ReadValue<Vector2>(),
            attack = _shoot.WasPressedThisFrame(),
            Hold = _shoot.IsPressed(),
            attackUp = _shoot.WasReleasedThisFrame(),
            Jump = _jump.WasPressedThisFrame(),
        };
    }

}

public struct FrameInput
{
    public Vector2 Move;
    public bool attack;
    public bool Hold;
    public bool attackUp;
    public bool Jump;
}
