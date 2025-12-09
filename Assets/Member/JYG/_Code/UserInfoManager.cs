using System;
using System.Collections.Generic;
using UnityEngine;
using YGPacks;

namespace Member.JYG._Code
{
    public class UserInfoManager : Singleton<UserInfoManager>
    {
        [SerializeField] private int currentState;
        
        public Sprite CurrentRocket { get; private set; }
        
        public List<Sprite> Rockets { get; private set; }
        

        protected override void Awake()
        {
            base.Awake();
            GameManager.Instance.OnClear += ClearThisStage;
            DontDestroyOnLoad(this);
            InitData();
        }

        private void InitData()
        {
            CurrentRocket = Rockets[PlayerPrefs.GetInt("Rocket", 0)];
        }

        public void ClearThisStage(int stageNum)
        {
            PlayerPrefs.SetInt($"{stageNum}Stage", PlayerPrefs.GetInt($"{stageNum}Stage") + 1);
        }

        private void OnApplicationQuit()
        {
            DataSave();
        }

        public void DataSave()
        {
            foreach (Sprite sprite in Rockets)
            {
                if (sprite == CurrentRocket)
                {
                    PlayerPrefs.SetInt("CurrentRocket", Rockets.IndexOf(sprite));
                }
            }
        }
    }
}