using TMPro;
using UnityEngine;
namespace PCM
{
    public class PlayTime : YGPacks.Singleton<PlayTime>
    {
        public float CurrentTime => Time.time - StartTime;

        private float StartTime;
        private TextMeshProUGUI time;

        public bool doCount { get; set; } = true;

        private void Start()
        {
            time = GetComponent<TextMeshProUGUI>();
            StartTime = Time.time;
        }

        void Update()
        {
            if (doCount == false) return;
            float t = Time.time - StartTime;
            time.text = t.ToString("F2") + 's';
        }
    }
}
