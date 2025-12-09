using csiimnida.CSILib.SoundManager.RunTime;
using System;
using UnityEngine;
using YGPacks; 

namespace Member.JYG._Code
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action<int> OnClear;
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

        private void Update()
        {
            if (player.transform.position.y >= StageSO.MapDistance)
            {
                OnClear?.Invoke(StageSO.StageNumber);
                TimeManager.Instance.StopTime();
            }
        }
    }
}
