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

        public Action OnDashPressed;
        public Action OnBrakePressed;
        
        public Action OnLeftClicked;
        public Action OnRightClicked;
        public Action OnWheelBtnClicked;

        public bool IsBraking { get; private set; }

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
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsBraking = true;
            }

            if (context.canceled)
            {
                IsBraking = false;
            }
            OnBrakePressed?.Invoke();
        }

        public void OnBoost(InputAction.CallbackContext context)
        {
            OnDashPressed?.Invoke();
        }

        public void OnLeftClick(InputAction.CallbackContext context)  
        {
            OnLeftClicked?.Invoke();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            OnRightClicked?.Invoke();
        }

        public void OnWheelClick(InputAction.CallbackContext context)
        {
            OnWheelBtnClicked?.Invoke();
        }
    }
}
