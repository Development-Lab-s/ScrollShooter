using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;
using YGPacks; 

namespace Member.JYG._Code
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public StageSO StageSO { get; private set; }
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
            SoundManager.Instance.PlaySound(StageSO.StageBGM);
        }

        private void Start()
        {
            SetCursorActive(false);
        }

        public void SetCursorActive(bool isActive)
        {
            InputControlleManager.Instance.ChangePlayerInputActive(!isActive);
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
