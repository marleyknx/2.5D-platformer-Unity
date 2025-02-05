using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Grounded _playerCollision;
    PlayerJump _playerJump;
    Movement _playerMovement;
    PlayerInputHandler _playerInputHandler;
    FrameInput _frameInput;
    Flip _playerFlip;
    PlayerAnimator _playerAnimator;
    PlayerAttack _playerAttack;
   

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _playerCollision = GetComponent<Grounded>();
        _playerJump = GetComponent<PlayerJump>();   
        _playerMovement = GetComponent<Movement>();
         _playerFlip = GetComponent<Flip>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAttack = GetComponent<PlayerAttack>();
        
    }
    private void Update()
    {
        _frameInput = _playerInputHandler.FrameInput;
        //animation
        _playerAnimator.UpdateSpeedAnimation(_frameInput.Move.x);
       _playerAnimator.SetIsHold(_playerAttack._isCharging);
        _playerAnimator.SetAttack2(_playerAttack.attack2);
        _playerAnimator.SetAttack3(_playerAttack.attack3);

     
      

        _playerMovement.SetDirection(_frameInput.Move.x);

        if (_frameInput.Jump)
        {
        _playerAnimator.SetJumping();
         _playerJump.ExecuteJump(IsGrounded());

        }


       
        _playerFlip.FlipCharacter(_frameInput.Move.x);

    }

  

   


    public bool IsGrounded() => _playerCollision.isgrounded;
}
