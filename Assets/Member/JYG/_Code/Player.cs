using System;
using System.Collections;
using csiimnida.CSILib.SoundManager.RunTime;
using Member.JYG.Input;
using UnityEngine;
using UnityEngine.Events;

namespace Member.JYG._Code
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class Player : MonoBehaviour
    {
        [field:SerializeField] public PlayerInputSO PlayerInputSO { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public CircleCollider2D Collider { get; private set; }
        
        [field:SerializeField] public float MaxSpeed { get; private set; }
        [field:SerializeField] public float ReverseForce { get; private set; }
        [field:SerializeField] public float BrakePower { get; private set; }
        [field: SerializeField] public float MovePower { get; private set; } //User's acceleration power (Move Force)
        [field: SerializeField] public float DashCoolTime { get; private set; } 
        
        [field: SerializeField] public float DashDuration { get; private set; } 
        
        [field: SerializeField] public float YSpeed { get; private set; } 
        [field: SerializeField] public float YSpeedAddForce { get; private set; } 
        [field:SerializeField] public bool IsBoosting { get; private set; }
        public bool playerInCamera = true;
        public UnityEvent<float> onBoost;
        public UnityEvent onBoostFailed;
        public float OriginalSpeed { get; private set; }
        
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
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            
            Rigidbody2D.gravityScale = 0;
            Rigidbody2D.linearVelocityY = YSpeed;
            
            _radius = Collider.radius;
            OriginalSpeed = MaxSpeed;
        }

        private void Start()
        {
            PlayerInputSO.OnDashPressed += HandleDashPressed;
            PlayerInputSO.OnDashBlocked += HandleDashBlocked;
            PlayerInputSO.OnBrakePressed += HandleBraked;
        }

        private void HandleBraked()
        {
            SoundManager.Instance.PlaySound("Braking");
        }

        private void HandleDashBlocked()
        {
            if (!IsBoosting)
            {
                onBoostFailed?.Invoke();
                SoundManager.Instance.PlaySound("BoostFail");
            }
        }

        private void HandleDashPressed()
        {
            StartCoroutine(PlayerDash());
        }

        private void OnDestroy()
        {
            PlayerInputSO.OnDashPressed -= HandleDashPressed;
            PlayerInputSO.OnDashBlocked -= HandleDashBlocked;
            PlayerInputSO.OnBrakePressed -= HandleBraked;
        }

        private IEnumerator PlayerDash()
        {
            IsBoosting = true;
            SoundManager.Instance.PlaySound("Boosting");
            StartCoroutine(SetSpeedWithTime(25f, 1f));
            yield return new WaitForSeconds(DashDuration);
            StartCoroutine(SetSpeedWithTime(OriginalSpeed, 1f));
            IsBoosting = false;
            yield return new WaitForSeconds(DashCoolTime);
            PlayerInputSO.canDash = true;
        }

        private void FixedUpdate()
        {
            SetXMove(XVelocity);
        }
        private void Update()
        {
            SetVelocity(PlayerInputSO.IsBraking); //Setting my speed Method
            if (playerInCamera)
            {
                SetPlayerPositionInCamera();
            }
        }
        
        private void SetVelocity(bool isBrake) //Use in Update
        {
            if (isBrake)
            {
                XVelocity = 0;
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

        private IEnumerator SetSpeedWithTime(float speed, float duration)
        {
            if (MaxSpeed < speed)
            {
                onBoost?.Invoke(DashDuration);
                while(MaxSpeed < speed)
                {
                    MaxSpeed += Time.deltaTime / duration * 10;
                    Rigidbody2D.linearVelocityY += YSpeedAddForce * Time.deltaTime;
                    yield return null;
                }
                MaxSpeed = speed;
                Rigidbody2D.linearVelocityY = YSpeed + YSpeedAddForce;
            }
            else
            {
                while(MaxSpeed > speed)
                {
                    MaxSpeed -= Time.deltaTime / duration * 10;
                    Rigidbody2D.linearVelocityY -= YSpeedAddForce * Time.deltaTime;
                    yield return null;
                }
                MaxSpeed = speed;
                Rigidbody2D.linearVelocityY = YSpeed;
            }
        }

        public void SetMaxSpeed(float targetMaxSpeed, float duration)
        {
            float target = MaxSpeed - targetMaxSpeed; //30, 25 -> 5
            StartCoroutine(SpeedChange(target, duration));
        }

        private IEnumerator SpeedChange(float force, float duration)
        {
            while (true)
            {
                MaxSpeed += force / duration;
                yield return null;
            }
        }

        public void StopXYVelocity()
        {
            Rigidbody2D.linearVelocityY = 0;
            XVelocity = 0;
        }
    }
}