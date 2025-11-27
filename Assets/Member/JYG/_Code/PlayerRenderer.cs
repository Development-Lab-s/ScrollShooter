using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Member.JYG._Code
{
    public class PlayerRenderer : MonoBehaviour
    {
        private Player _player;
        private Volume _volume;
        [field: SerializeField] public float Offset { get; private set; }
        [field:SerializeField] public float MaxDegree { get; private set; }
        
        private Vector3 _currentPosition;
        
        private void Awake()
        {
            _player = GetComponent<Player>();
            _volume = FindFirstObjectByType<Volume>();
        }

        private void Start()
        {
            _player.PlayerInputSO.OnDashPressed += HandleScreenEffect;
        }

        private void OnDestroy()
        {
            _player.PlayerInputSO.OnDashPressed -= HandleScreenEffect;
        }

        private void HandleScreenEffect()
        {
            if (_volume.profile.TryGet<LensDistortion>(out LensDistortion lens))
            {
                lens.active = true;
                StartCoroutine(OffDistortion(lens));
            }
        }

        private IEnumerator OffDistortion(LensDistortion lens)
        {
            yield return new WaitForSeconds(_player.DashDuration);
            lens.active = false;
        }

        private void Update()
        {
            SetRotation(gameObject, _player.XVelocity); //이동속도를 받아와서 나를 돌린다
        }
    
        private void SetRotation(GameObject target, float velocity)
        {
            float zValue = Mathf.Lerp(-MaxDegree, MaxDegree, 0.5f + velocity / _player.MaxSpeed * 0.5f) * -1;
            zValue = Mathf.MoveTowardsAngle(transform.eulerAngles.z, zValue, Time.deltaTime * 450f);
            transform.rotation = Quaternion.Euler(0, 0, zValue);
        }
    }
}
