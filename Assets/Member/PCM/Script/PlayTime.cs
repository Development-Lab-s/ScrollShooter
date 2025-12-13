using TMPro;
using UnityEngine;
namespace PCM
{
    public class PlayTime : MonoBehaviour
    {
        private TextMeshProUGUI time;
        private float StartTime;
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
