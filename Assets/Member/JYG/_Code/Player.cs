using System;
using System.Collections;
using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using Member.JYG.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

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
        //[field: SerializeField] public float ReverseForce { get; private set; }
        //[field: SerializeField] public float BrakePower { get; private set; }
        [field: SerializeField] public float MovePower { get; private set; } //User's acceleration power (Move Force)
        [field: SerializeField] public float DashCoolTime { get; private set; }
        [field: SerializeField] public float DashDuration { get; private set; }

        //[field: SerializeField] public float YSpeed { get; private set; }
        //[field: SerializeField] public float OriginYSpeed { get; private set; }
        //[field: SerializeField] public float YSpeedAddForce { get; private set; }
        [field: SerializeField] public bool IsBoosting { get; private set; }

        [field: SerializeField] public MoveDataSO MoveData { get; private set; }
        [SerializeField] private int maxSpeedY;
        [SerializeField] private ParticleFeedback clearFeedback;
        public int MaxSpeedY
        {
            get => Mathf.Max(maxSpeedY, MoveData.minSpeed);
            private set => maxSpeedY = value;
        }

        private CustomTween _dashCoroutine;
        [SerializeField] private ParticleSystem boostParticles;
        [SerializeField] private float _currentVelocityY = 3f;

        public bool playerInCamera = true;
        public UnityEvent<float> OnVelocityChanged;
        public UnityEvent<float> onBoost;
        public UnityEvent onStopBoost;
        public UnityEvent onBoostFailed;
        //public float OriginalSpeed { get; private set; }
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
            //Rigidbody2D.linearVelocityY = YSpeed;

            _radius = Collider.radius;
            //OriginalSpeed = MaxSpeedX;
            maxSpeedY = MoveData.maxSpeed;
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

        private float CalculateSpeedY()
        {
            if (MaxSpeedY > _currentVelocityY)
                _currentVelocityY += MoveData.acceleration * Time.deltaTime;
            else if (MaxSpeedY < _currentVelocityY)
                _currentVelocityY -= MoveData.deacceleration * Time.deltaTime;

            return _currentVelocityY;
        }

        private void MoveY()
        {
            Rigidbody2D.linearVelocityY = _currentVelocityY;
        }
        private void Update()
        {
            if (_currentVelocityY != MaxSpeedY)
                _currentVelocityY = CalculateSpeedY();
            SetVelocity(PlayerInputSO.IsBraking || _isKnock); //Setting my speed Method
            if (playerInCamera)
            {
                SetPlayerPositionInCamera();
            }
        }

        private void FixedUpdate()
        {
            OnVelocityChanged?.Invoke(_currentVelocityY);
            SetXMove(XVelocity);
            MoveY();
        }
        public void SetMaxSpeed(int newMaxSpeed)
        {
            this.maxSpeedY = newMaxSpeed;
        }
        public CustomTween SetMaxSpeed(int newMaxSpeed, float duration, Action callback = default)
        {
            this.maxSpeedY += newMaxSpeed;
            callback += () => this.maxSpeedY -= newMaxSpeed;
            return new CustomTween(StartCoroutine(DelayCallCoroutine(duration, callback)), callback);
        }
        public void PlayerDash()
        {
            if (_isKnock == false && IsBoosting == false)
                OnDash();
        }
        public void OnDash()
        {
            DashColorChanger colorChanger = GetComponent<DashColorChanger>();
            PlayerInputSO.canDash = false;
            colorChanger.DashStateIsTrue(PlayerInputSO.canDash);

            boostParticles.Stop();
            ParticleSystem.MainModule main = boostParticles.main;
            main.duration = DashDuration - 0.5f;
            onBoost?.Invoke(DashDuration);
            SoundManager.Instance.PlaySound("Boosting");

            IsBoosting = true;
            _dashCoroutine = SetMaxSpeed(MoveData.dashSpeed, DashDuration, () =>
            {
                IsBoosting = false;
                StartCoroutine(DelayCallCoroutine(DashCoolTime, () =>
                {
                    PlayerInputSO.canDash = true;
                    colorChanger.DashStateIsTrue(PlayerInputSO.canDash);
                }));
            });

        }

        private IEnumerator DelayCallCoroutine(float delayTime, Action callBack = default)
        {
            yield return new WaitForSeconds(delayTime);
            callBack?.Invoke();
        }

        //private IEnumerator PlayerDashCoroutine()
        //{
        //    StartCoroutine(SetSpeedWithTime(25f, 1f));
        //    yield return new WaitForSeconds(DashDuration);
        //    StartCoroutine(SetSpeedWithTime(OriginalSpeed, 1f));
        //    //StartCoroutine(SetYSpeedWithTime(OriginYSpeed * 2, 1.5f, OriginYSpeed));
        //    //StartCoroutine(SetYSpeedWithTime(OriginYSpeed, 1f, OriginYSpeed));
        //}
        private void SetVelocity(bool isBrake) //Use in Update
        {
            if (isBrake)
            {
                XVelocity = 0;
                return;
            }
            float moveDir = PlayerInputSO.XMoveDir;
            ToMoveX(moveDir);
        }

        private void ToMoveX(float moveXDir)
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

        public void SetMaxSpeedX(float targetMaxSpeed, float duration)
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
            MaxSpeedX = 0;
            MaxSpeedY = 0;
            _xVelocity = 0;
            _currentVelocityY = 0;
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

        //public void SetYSpeed(float speed)
        //{
        //    YSpeed = speed;
        //    Rigidbody2D.linearVelocityY = speed;
        //}

        //private IEnumerator SetYSpeedWithTime(float speed, float duration, float originSpeed)
        //{
        //    float aSpeed = YSpeed - speed;
        //    OriginalSpeed = speed;
        //    if (YSpeed > speed)
        //        while (YSpeed >= speed)
        //        {
        //            SetYSpeed(YSpeed - Time.deltaTime / duration * aSpeed);
        //            yield return null;
        //        }
        //    else if (YSpeed < speed)
        //        while (YSpeed <= speed)
        //        {
        //            SetYSpeed(YSpeed - Time.deltaTime / duration * aSpeed);
        //            yield return null;
        //        }
        //    SetYSpeed(speed);
        //    OriginYSpeed = originSpeed;
        //}
        public void InitMySkin(string skinName)
        {
            if (String.IsNullOrEmpty(skinName) || skinList.skinList[1] == null) SpriteRenderer.sprite = skinList.skinList[0].skin;
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
            var dash = _dashCoroutine?.Complete();
            if (null != dash)
            {
                StopCoroutine(dash);
                _dashCoroutine = null;
            }

            SetVelocity(true);
            _isKnock = true;
            Rigidbody2D.linearVelocityY = 0;
            Rigidbody2D.AddForceY(-knockPower, ForceMode2D.Impulse);

            _currentVelocityY = Rigidbody2D.linearVelocityY;

            SetMaxSpeed(-maxSpeedY, knockTime, () => _isKnock = false);
        }

        public void OnClear()
        {
            clearFeedback.StartParticle();
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
        if (coroutine != null)
            onComplete?.Invoke();
        return coroutine;
    }
}