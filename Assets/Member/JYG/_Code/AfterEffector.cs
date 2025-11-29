using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Member.JYG._Code
{
    public class AfterEffector : MonoBehaviour
    {
        [SerializeField] private Volume dashEffectVolume;
        //private Player Player => GameManager.Instance.Player;
    
        private Player player => GameManager.Instance.Player; 
        private void Awake()
        {
            GameManager.Instance.Player.PlayerInputSO.OnDashPressed += HandlePlayerDashEffect;
        }

        private void HandlePlayerDashEffect()
        {
            //StartCoroutine(EffectApply());
        }
        private void OnDestroy()
        {
            GameManager.Instance.Player.PlayerInputSO.OnDashPressed -= HandlePlayerDashEffect;
        }
/*
        private IEnumerator EffectApply()
        {
            
        }*/

    }
}
