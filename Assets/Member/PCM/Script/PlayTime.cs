using TMPro;
using UnityEngine;
namespace PCM
{
    public class PlayTime : YGPacks.Singleton<PlayTime>
    {
        public float CurrentTime => Time.time - StartTime;

        private float StartTime;
        private TextMeshProUGUI time;

        private void Start()
        {
            time = GetComponent<TextMeshProUGUI>();
            StartTime = Time.time;
        }

        void Update()
        {
            float t = Time.time - StartTime;
            time.text = t.ToString("F2") + 's';
        }
    }
}
