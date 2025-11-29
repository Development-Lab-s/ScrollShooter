using System;
using Member.JYG.Input;
using UnityEngine;
using UnityEngine.Rendering;

namespace Member.JYG._Code
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class Player : MonoBehaviour
    {
        [field:SerializeField] public PlayerInputSO PlayerInputSO { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public CircleCollider2D Collider { get; private set; }
        [field:SerializeField] public float MaxSpeed { get; private set; }
        [field:SerializeField] public float ReverseForce { get; private set; }
        [field:SerializeField] public float BrakePower { get; private set; }
        [field: SerializeField] public float MovePower { get; private set; } //User's acceleration power (Move Force)
        [field: SerializeField] public float DashCoolTime { get; private set; } 
        [field: SerializeField] public float DashDuration { get; private set; } 
        
        private float _xVelocity;
        

        private float _radius;

        public float XVelocity //Player's real move speed
        {
            get => _xVelocity;
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

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
            
            Rigidbody2D.gravityScale = 0;
            Rigidbody2D.linearVelocityY = 5f;
            
            _radius = Collider.radius;
        }
        private void FixedUpdate()
        {
            SetXMove(XVelocity);
        }
        private void Update()
        {
            SetVelocity(PlayerInputSO.IsBraking); //Setting my speed Method
            SetPlayerPositionInCamera();
        }
        
        private void SetVelocity(bool isBrake) //Use in Update
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
            ToMove(moveDir);
        }

        private void ToMove(float moveXDir)
        {
            if ((XVelocity > 0.1f && moveXDir < 0.5f) || (XVelocity < -0.1f && moveXDir > 0.5f))
            {
                XVelocity += Time.deltaTime * MovePower * 2 * moveXDir; //Power x2
            }
                
            XVelocity += Time.deltaTime * MovePower * moveXDir; //현재 이속 설정
        }
        private void SetXMove(float speed) //Use in FixedUpdate
        {
            Rigidbody2D.linearVelocityX = speed; //Set my speed
        }

        private void SetPlayerPositionInCamera()
        {
            if (Camera.main.WorldToViewportPoint(new Vector3(transform.position.x + _radius, 0)).x > 1f)
            {
                Vector3 newPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

                Vector3 playerPosition = transform.position;
                playerPosition.x = newPosition.x - _radius;
                transform.position = playerPosition;
            }
            else if (Camera.main.WorldToViewportPoint(new Vector3(transform.position.x - _radius, 0)).x < 0f)
            {
                Vector3 newPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

                Vector3 playerPosition = transform.position;
                playerPosition.x = newPosition.x + _radius;
                transform.position = playerPosition;
            }
        }
    }
}