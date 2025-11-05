using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

namespace Member.JYG.Input
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO", order = 15)]
    public class PlayerInputSO : ScriptableObject, IPlayerActions
    {
        private PlayerInput _playerInput;

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
            }
            _playerInput.Player.Enable();
            _playerInput.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            _playerInput.Player.Disable();
        }
        
        public float XMoveDir { get; private set; }
        public void OnMove(InputAction.CallbackContext context)
        {
            XMoveDir = context.ReadValue<float>();
            Debug.Log(XMoveDir);
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            
        }
    }
}
