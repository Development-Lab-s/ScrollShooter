using System;
using UnityEngine;
using UnityEngine.UI;

namespace Member.JYG._Code
{
    public class RunningLengthDetector : MonoBehaviour
    {
        private Player _myPlayer;
        [SerializeField] private float mapLength = 100f;
        [SerializeField] private Image amountImage;
        private void Start()
        {
            _myPlayer = GameManager.Instance.Player;
        }

        private void Update()
        {
            amountImage.fillAmount = _myPlayer.transform.position.y / mapLength;
        }
    }
}
