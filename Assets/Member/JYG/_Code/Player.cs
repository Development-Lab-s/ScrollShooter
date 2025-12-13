using System;
using System.Collections;
using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using Member.JYG.Input;
using UnityEngine;
using UnityEngine.Events;

namespace Member.JYG._Code
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class Player : MonoBehaviour, IPlayer
    {
        [field: SerializeField] public PlayerInputSO PlayerInputSO { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public CircleCollider2D Collider { get; private set; }
        [SerializeField] private GameObject trail;

        [field: SerializeField] public float MaxSpeedX { get; private set; }
        [field: SerializeField] public float ReverseForce { get; private set; }
        [field: SerializeField] public float BrakePower { get; private set; }
        [field: SerializeField] public float MovePower { get; private set; } //User's acceleration power (Move Force)
        [field: SerializeField] public float DashCoolTime { get; private set; }
        [field: SerializeField] public float DashDuration { get; private set; }

        [field: SerializeField] public float YSpeed { get; private set; }
        [field: SerializeField] public float OriginYSpeed { get; private set; }
        [field: SerializeField] public float YSpeedAddForce { get; private set; }
        [field: SerializeField] public bool IsBoosting { get; private set; }

        private CustomTween _dashCoroutine;
        [SerializeField] private ParticleSystem boostParticles;

        public bool playerInCamera = true;
        public UnityEvent<float> onBoost;
        public UnityEvent onStopBoost;
        public UnityEvent onBoostFailed;
        public float OriginalSpeed { get; private set; }
        public bool IsInvincible { get; private set; }

        private float _xVelocity;

        private float _radius;
        public bool IsDash => IsBoosting;
        private bool _isKnock;
        private HitSystem _hitSystem;
        Tween _coolTween;
        public float XVelocity //Player's real move speed
        {
            get => _xVelocity;
            private set
            {
                if (Mathf.Abs(value) < MaxSpeedX)
                {
                    _xVelocity = value;
                }
                else
                {
                    _xVelocity = Mathf.Sign(value) * MaxSpeedX;
                }
            }
        }

        public PTY.Scripts.SO.SkinListSO skinList;

        public void Nyan()
        {
            trail.SetActive(true);
        }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _hitSystem = GetComponent<HitSystem>();

            Rigidbody2D.gravityScale = 0;
            Rigidbody2D.linearVelocityY = YSpeed;

            _radius = Collider.radius;
            OriginalSpeed = MaxSpeedX;
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
            if (_isKnock) return;
            PlayerInputSO.canDash = true;
            PlayerDash();
        }

        private void OnDestroy()
        {
            PlayerInputSO.OnDashPressed -= HandleDashPressed;
            PlayerInputSO.OnDashBlocked -= HandleDashBlocked;
            PlayerInputSO.OnBrakePressed -= HandleBraked;
        }
        private void PlayerDash()
        {
            onBoost?.Invoke(DashDuration);
            SoundManager.Instance.PlaySound("Boosting");
            ParticleSystem.MainModule main = boostParticles.main;
            main.duration = DashDuration + 1;

            PlayerInputSO.canDash = false;
            IsBoosting = true;

            _dashCoroutine = new CustomTween(StartCoroutine(PlayerDashCoroutine()), () =>
            {
                IsBoosting = false;
                SetYSpeed(OriginalSpeed);
                StartCoroutine(PlayerDashCool(DashCoolTime));
            });
        }
        private IEnumerator PlayerDashCool(float coolTime)
        {
            yield return new WaitForSeconds(DashCoolTime);
            PlayerInputSO.canDash = true;
        }

        private IEnumerator PlayerDashCoroutine()
        {
            StartCoroutine(SetSpeedWithTime(25f, 1f));
            yield return new WaitForSeconds(DashDuration);
            StartCoroutine(SetSpeedWithTime(OriginalSpeed, 1f));
            //StartCoroutine(SetYSpeedWithTime(OriginYSpeed * 2, 1.5f, OriginYSpeed));
            //StartCoroutine(SetYSpeedWithTime(OriginYSpeed, 1f, OriginYSpeed));
        }
        private void FixedUpdate()
        {
            SetXMove(XVelocity);
        }

        private void Update()
        {
            SetVelocity(PlayerInputSO.IsBraking || _isKnock); //Setting my speed Method
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
            float offset = 0.5f * 0.78f;
            if (Camera.main.WorldToViewportPoint(new Vector3(transform.position.x + _radius, 0)).x > 0.5f + offset)
            {
                Vector3 newPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f + offset, 0, 0));

                Vector3 playerPosition = transform.position;
                playerPosition.x = newPosition.x - _radius;
                transform.position = playerPosition;
            }
            else if (Camera.main.WorldToViewportPoint(new Vector3(transform.position.x - _radius, 0)).x < 0.5f - offset)
            {
                Vector3 newPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f - offset, 0, 0));

                Vector3 playerPosition = transform.position;
                playerPosition.x = newPosition.x + _radius;
                transform.position = playerPosition;
            }
        }

        private IEnumerator SetSpeedWithTime(float speed, float duration)
        {
            float aSpeed = MaxSpeedX - speed;
            if (MaxSpeedX < speed)
            {
                while (MaxSpeedX < speed)
                {
                    MaxSpeedX -= Time.deltaTime / duration * aSpeed;
                    yield return null;
                }
                MaxSpeedX = speed;
            }
            else
            {
                while (MaxSpeedX > speed)
                {
                    MaxSpeedX -= Time.deltaTime / duration * aSpeed;
                    yield return null;
                }
                MaxSpeedX = speed;
            }
        }

        public void SetMaxSpeed(float targetMaxSpeed, float duration)
        {
            float target = MaxSpeedX - targetMaxSpeed; //30, 25 -> 5
            StartCoroutine(SpeedChange(target, duration));
        }

        public void SetPower(float value)
        {
            MovePower = value;
        }

        private IEnumerator SpeedChange(float force, float duration)
        {
            while (true)
            {
                MaxSpeedX += force / duration;
                yield return null;
            }
        }

        public void StopXYVelocity()
        {
            Rigidbody2D.linearVelocityY = 0;
            XVelocity = 0;
        }

        public void OnInvincible(float invincibleTime)
        {
            IsInvincible = true;
            this.SpriteRenderer.material.SetFloat("_FadingFade", 1);

            _coolTween?.Kill();
            TweenCallback callback = () =>
            {
                this.SpriteRenderer.material.SetFloat("_FadingFade", 0);
                IsInvincible = false;
            };

            _coolTween = DOVirtual.DelayedCall(invincibleTime, callback, false);
        }

        public void TakeDamage(int dmg)
        {
            if (false == IsInvincible)
                _hitSystem.Life -= dmg;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IContactable block)) block.TryContact(new ContactInfo(this));
            if (collision.TryGetComponent(out IUseable useable)) useable.Use(new UseableInfo(this));
        }

        public void SetYSpeed(float speed)
        {
            YSpeed = speed;
            Rigidbody2D.linearVelocityY = speed;
        }

        private IEnumerator SetYSpeedWithTime(float speed, float duration, float originSpeed)
        {
            float aSpeed = YSpeed - speed;
            OriginalSpeed = speed;
            if (YSpeed > speed)
                while (YSpeed >= speed)
                {
                    SetYSpeed(YSpeed - Time.deltaTime / duration * aSpeed);
                    yield return null;
                }
            else if(YSpeed < speed)
                while (YSpeed <= speed)
                {
                    SetYSpeed(YSpeed - Time.deltaTime / duration * aSpeed);
                    yield return null;
                }
            SetYSpeed(speed);
            OriginYSpeed = originSpeed;
        }
        public void InitMySkin(string skinName)
        {
            if (String.IsNullOrEmpty(skinName) && skinList.skinList[0] != null) SpriteRenderer.sprite = skinList.skinList[0].skin;
            foreach (SkinSO skin in skinList.skinList)
            {
                if (skinName == skin.skinName)
                {
                    SpriteRenderer.sprite = skin.skin;
                    break;
                }
            }
        }
        public void OnKnockback(float knockPower, float knockTime)
        {
            if (_isKnock) return;
            if (null != _dashCoroutine?.Complete())
                StopCoroutine(_dashCoroutine.Complete());


            SetVelocity(true);
            StartCoroutine(KnockbackWaithTime(knockPower,knockTime));
        }

        private IEnumerator KnockbackWaithTime(float knockPower,float knockTime)
        {
            _isKnock = true;
            Rigidbody2D.AddForceY(-knockPower, ForceMode2D.Impulse);
            yield return new WaitForSeconds(knockTime);
            SetYSpeed(Rigidbody2D.linearVelocityY);
            StartCoroutine(SetYSpeedWithTime(OriginYSpeed, knockTime, OriginYSpeed));
            _isKnock = false;
        }
    }
}
public class CustomTween
{
    Coroutine coroutine;
    public Action onComplete;
    public CustomTween(Coroutine coroutine, Action onComplete = default)
    {
        this.coroutine = coroutine;
        this.onComplete = onComplete;
    }
    public Coroutine Complete()
    {
        onComplete?.Invoke();
        return coroutine;
    }
}