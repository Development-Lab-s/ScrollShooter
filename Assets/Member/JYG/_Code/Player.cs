using System;
using Member.JYG.Input;
using UnityEngine;

namespace Member.JYG._Code
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
    public class Player : MonoBehaviour
    {
        [field:SerializeField] public PlayerInputSO PlayerInputSO { get; private set; }

        #region PlayerMovement
        public Rigidbody2D Rigidbody2D { get; private set; }
        [field:SerializeField] public float MaxSpeed { get; private set; }
        [field:SerializeField] public float ReverseForce { get; private set; }
        [field:SerializeField] public float BrakePower { get; private set; }

        private Vector3 _currentPosition;

        private bool _canLeftMoving = true;
        private bool _canRightMoving = true;

        private float _xVelocity;

        public float XVelocity //Player의 진짜 이동속도
        {
            get
            {
                return _xVelocity;
            }
            private set
            {
                if (Mathf.Abs(value) < MaxSpeed)
                {
                    _xVelocity = value;
                }
                else
                {
                    _xVelocity = Mathf.Sign(value) * MaxSpeed;
                }
            }
            
        }
        [field: SerializeField] public float MovePower { get; private set; } //유저가 가속할 때 더해주는 크기

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Rigidbody2D.gravityScale = 0;
            Rigidbody2D.linearVelocityY = 5f;
        }
        
        private void SetVelocity(bool isBrake) //Update에서 실행중
        {
            if (isBrake)
            {
                if (Mathf.Abs(XVelocity) < 0.1f)
                {
                    XVelocity = 0;
                    return;
                }
                XVelocity = Mathf.Lerp(XVelocity, 0, Time.deltaTime * BrakePower);
                
                return;
            }
            
            float moveDir = PlayerInputSO.XMoveDir;
            if (moveDir == 1 && _canRightMoving) //우측으로 이동한다.
            {
                if(XVelocity < -0.1f) // 우측으로 이동하는 도중에 좌측으로 이동하려 한다.
                    XVelocity += Time.deltaTime * MovePower * 2 * moveDir; //파워 2배로 증가
                /*XVelocity = 0;*/
                
                XVelocity += Time.deltaTime * MovePower * moveDir; //현재 이속 설정
                _canLeftMoving = true;
            }
            else if (moveDir == -1 && _canLeftMoving) //좌측으로 이동한다.
            {
                if(XVelocity > 0.1f) // 좌측으로 이동하는 도중에 우측으로 이동하려 한다.
                    XVelocity +=  Time.deltaTime * MovePower * 2 * moveDir; //파워 2배로 증가
                /*    XVelocity = 0;*/
                
                XVelocity += Time.deltaTime * MovePower * moveDir;
                _canRightMoving = true;
            }

            //겹치는 코드가 많은데 어떻게 잘 해결해보자
        }

        private void FixedUpdate()
        {
            SetXMove(XVelocity);
        }


        private void SetXMove(float speed) //FixedUpdate에서 실행중
        {
            Rigidbody2D.linearVelocityX = speed; //현재 이동속도를 받아와서 고정
        }
        #endregion
        
        #region Render
        public SpriteRenderer SpriteRenderer { get; private set; }
        [field:SerializeField] public float MaxDegree { get; private set; }

        //속도를 가져와서 속도가 높을수록 기울기를 크게.
        
        private void SetRotation(GameObject target, float velocity)
        {
            float zValue = Mathf.Lerp(-MaxDegree, MaxDegree, 0.5f + velocity / MaxSpeed * 0.5f) * -1;
            zValue = Mathf.MoveTowardsAngle(transform.eulerAngles.z, zValue, Time.deltaTime * 450f);
            transform.rotation = Quaternion.Euler(0, 0, zValue);
        }
        #endregion

        [field: SerializeField] public float Offset { get; private set; }

        private void Update()
        {
            SetVelocity(PlayerInputSO.IsBraking); //현재 이동속도를 설정한다.
            SetRotation(gameObject, XVelocity); //이동속도를 받아와서 나를 돌린다
            KeepViewport();
        }

        private void KeepViewport()
        {
            _currentPosition = Camera.main.WorldToViewportPoint(transform.position);

            if (_currentPosition.x < Offset)
            {
                _currentPosition.x = Offset;
                _canLeftMoving = false;
            }
            else if (_currentPosition.x >= Offset)
            {
                _canLeftMoving = true;
            }
            if (_currentPosition.x > 1f - Offset)
            {
                _currentPosition.x = 1f - Offset;
                _canRightMoving = false;
            }
            else if (_currentPosition.x <= 1f - Offset)
            {
                _canRightMoving = true;
            }

            transform.position = Camera.main.ViewportToWorldPoint(_currentPosition);
        }
    }
}