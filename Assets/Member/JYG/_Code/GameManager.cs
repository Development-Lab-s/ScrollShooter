using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YGPacks; 

namespace Member.JYG._Code
{
    public class GameManager : Singleton<GameManager>
    {
        public bool thereIsPlayer = false;
        public AudioSource inGameAudio { get; private set; }
        private AudioSource otherAudio;

        public event Action<int> OnClear;
        private bool cleared = false;

        [field: SerializeField] public StageSO StageSO { get; private set; }
        private Player player;
        
        [field:SerializeField] public SkinSO ClearSkin { get; private set; }
        [field:SerializeField] public SkinSO TimeSkin { get; private set; }
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
           inGameAudio = SoundManager.Instance.PlaySound(StageSO.StageBGM);
        }

        private void Start()
        {
            if (thereIsPlayer)
            {
                Player.InitMySkin(PlayerPrefs.GetString("userskin"));
            }
        }

        private void Update()
        {
            if (player != null && player.transform.position.y >= StageSO.MapDistance && cleared == false)
            {
                cleared = true;
                OnClear?.Invoke(SceneManager.GetActiveScene().buildIndex);
                player.OnClear();
                TimeManager.Instance.StopTime();
                if (PlayerPrefs.GetInt(ClearSkin.prefsName, 0) == 0)
                {
                    PlayerPrefs.SetInt(ClearSkin.prefsName, 1);
                }
                else if (TimeSkin != null)
                {
                    if (PlayerPrefs.GetInt(TimeSkin.prefsName, 0) == 0 && TimeSkin.targetTime >= Time.time)
                    {
                        PlayerPrefs.SetInt(TimeSkin.prefsName, 1);
                    }
                }
                
            }
        }

        public void ChangeInGameBGM()
        {
            if (otherAudio == null) return;
            Destroy(otherAudio.gameObject);
            inGameAudio.UnPause();
        }

        public void ChangeBGM(string key)
        {
            inGameAudio.Pause();
            otherAudio = SoundManager.Instance.PlaySound(key);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            DOTween.KillAll();
        }
    }
}
