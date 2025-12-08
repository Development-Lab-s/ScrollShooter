using TMPro;
using UnityEngine;
namespace PCM
{
    public class PlayTime : MonoBehaviour
    {
        private TextMeshProUGUI time;
        private void Start()
        {
            time = GetComponent<TextMeshProUGUI>();
        }
        void Update()
        {
            time.text = UnityEngine.Time.time.ToString("F2");
        }
    }
}
