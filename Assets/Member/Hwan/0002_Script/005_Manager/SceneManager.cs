using System;
using UnityEngine;


namespace Hwan
{
    public class SceneManager : MonoBehaviour
    {

        private static SceneManager _instance;
        public static SceneManager Instance
        {
            get
            {
                return _instance ??= new GameObject().AddComponent<SceneManager>();
            }
            private set
            {
                _instance = value;
            }
        }

        private ScreenFader _screenFader;

        [Range(0.1f, 3f)]
        [SerializeField] float fadeIn = 0.5f, fadeOut = 0.5f;
        private int _sceneIndex;
        public Action SceneLoadEnded;
        public void OnLoadScene(int sceneIndex)
        {
            if (_screenFader.IsSceneFading) return;
            _sceneIndex = sceneIndex;
            _screenFader.FadeInEnded += OnFadeInEnded;
            _screenFader.StartFadeIn(fadeIn);
        }
        private void OnFadeInEnded()
        {
            _screenFader.FadeInEnded -= OnFadeInEnded;
            LoadScene(_sceneIndex);
        }
        private void LoadScene(int sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += (_) => {
                _screenFader.StartFadeOut(fadeOut); Time.timeScale = 1;
            };
            SceneLoadEnded?.Invoke();
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _screenFader = gameObject.GetComponent<ScreenFader>();
                GameObject.DontDestroyOnLoad(this);
            }
            else if (_instance != this)
            {
                Destroy(this);
            }
        }
        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

    }
}
