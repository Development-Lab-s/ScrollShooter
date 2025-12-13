using UnityEngine;
using UnityEngine.UI;

namespace Member.JYG._Code
{
    public class RunningLengthDetector : MonoBehaviour
    {
        private Player _myPlayer;
        private float mapLength;
        [SerializeField] private Image amountImage;

        private void Start()
        {
            _myPlayer = GameManager.Instance.Player;
            mapLength = GameManager.Instance.StageSO.MapDistance;
        }

        private void Update()
        {
            amountImage.fillAmount = _myPlayer.transform.position.y / mapLength;
        }
    }
}