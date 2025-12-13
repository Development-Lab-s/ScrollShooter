using System;
using System.Collections.Generic;
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
        public bool rigthPerformed;

        private Dictionary<InteractiveType, bool> inputActiveDictionary = new();

        private void OnEnable()
        {
            canDash = true;
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
            }
            _playerInput.Player.Enable();
            _playerInput.Player.SetCallbacks(this);

            foreach (InteractiveType type in Enum.GetValues(typeof(InteractiveType)))
            {
                inputActiveDictionary.Add(type, true);
            }
            
        }

        private void OnDisable()
        {
            _playerInput.Player.Disable();
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (inputActiveDictionary[InteractiveType.Scroll] == false) return;
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
            if (inputActiveDictionary[InteractiveType.Forward] == false) return;
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
            if (inputActiveDictionary[InteractiveType.Back] == false) return;
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
            if (inputActiveDictionary[InteractiveType.Left] == false) return;

            OnLeftClicked?.Invoke();
            #region
            //if (context.performed)
            //{
            //    Debug.Log("tlqkf");
            //    OnLeftClicked?.Invoke();
            //    leftPerformed = context.performed;

            //}
            //else if(context.canceled)
            //{
            //    Debug.Log("µé¾î");
            //    leftPerformed = context.performed;
            //    Debug.Log(leftPerformed);
            //}
            #endregion

        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (inputActiveDictionary[InteractiveType.Right] == false) return;

            if (context.performed || context.canceled)
            {
                rigthPerformed = context.performed;
            }
            OnRightClicked?.Invoke();
        }

        public void OnWheelClick(InputAction.CallbackContext context)
        {
            if (inputActiveDictionary[InteractiveType.Middle] == false) return;
            if(context.performed)
                OnWheelBtnClicked?.Invoke();
        }
        
        public void SetInputTypeActive(InteractiveType type, bool active)
        {
            inputActiveDictionary[type] = active;
        }

        public void SetInputActive(bool active)
        {
            if (active == false)
            {
                _playerInput.Player.Disable();
                return;
            }
            _playerInput.Player.Enable();
        }
    }
}