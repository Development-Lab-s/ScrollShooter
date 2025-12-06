using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using YGPacks; 

namespace Member.JYG._Code
{
    public class GameManager : Singleton<GameManager>
    {
        private Player player;
        public Player Player 
        { 
            get
            {
                if (player == null) player = FindFirstObjectByType<Player>();
                return player;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            SetCursorLock(false);
        }

        public void SetCursorLock(bool isActive)
        {
            Player.PlayerInputSO.ChangeInputState(!isActive);
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
