using UnityEngine;
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
            SetCursorLock(true);
        }

        public void SetCursorLock(bool isActive)
        {
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
