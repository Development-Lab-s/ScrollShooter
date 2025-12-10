using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YGPacks;

namespace Member.JYG._Code
{
    public class SaveManager : Singleton<SaveManager>
    {
        [SerializeField] private int currentState;
        
        public Sprite CurrentRocket { get; private set; }
        
        public List<Sprite> SkinInfo { get; private set; }
        private Dictionary<string, Sprite> _skinInfo = new Dictionary<string, Sprite>();
        

        protected override void Awake()
        {
            base.Awake();
            foreach (Sprite item in SkinInfo)
            {
                
            }
            SceneManager.sceneLoaded += SetMySkin;
            DontDestroyOnLoad(this);
        }

        private void SetMySkin(Scene arg0, LoadSceneMode arg1)
        {
            if (GameManager.Instance == null)
            {
                return;
            }

            //PlayerPrefs.GetString("userskin")
        }
    }
}