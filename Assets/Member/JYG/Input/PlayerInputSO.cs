using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static PlayerInput;

namespace Member.JYG.Input
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO", order = 15)]
    public class PlayerInputSO : ScriptableObject, IPlayerActions
    {
        private PlayerInput _playerInput;

        public Action OnDashPressed;
        public Action OnDashBlocked;
        public Action OnBrakePressed;
        
        public Action OnLeftClicked;
        public Action OnRightClicked;
        public Action OnWheelBtnClicked;

        public Action OnWheeling;
        public bool IsBraking { get; private set; }
        public int XMoveDir { get; private set; }
        public float wheelDeltaValue { get; private set; }

        private float _prevDashTime;
        public bool canDash = true;
        public bool isUIInput = false;

        private void OnEnable()
        {
            canDash = true;
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
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                XMoveDir = (int)context.ReadValue<float>();
                OnWheeling?.Invoke();
            }

            if (context.canceled)
            {
                XMoveDir = 0;
            }
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnBrakePressed?.Invoke();
                IsBraking = true;
            }

            if (context.canceled)
            {
                IsBraking = false;
            }
            
        }

        public void OnBoost(InputAction.CallbackContext context)
        {
            if (context.performed == false) return;
            if (isUIInput == true)
            {
                OnDashPressed?.Invoke();
                return;
            }
            if (canDash == true)
            {
                OnDashPressed?.Invoke();
                canDash = false;
            }
            else
            {
                OnDashBlocked?.Invoke();
            }
        }

        public void OnLeftClick(InputAction.CallbackContext context)  
        {
            if(context.performed)
                OnLeftClicked?.Invoke();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnRightClicked?.Invoke();
        }

        public void OnWheelClick(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnWheelBtnClicked?.Invoke();
        }

        public void OffInput()
        {
            _playerInput.Player.Disable();
        }

        public void ActiveInput()
        {
            _playerInput.Player.Enable();
        }
    }
}