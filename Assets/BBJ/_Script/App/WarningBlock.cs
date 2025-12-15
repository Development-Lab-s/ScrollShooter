using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class WarningBlock : BlockBase, IExplosion, IContactable
{
    [SerializeField] private OverlapDataSO chackPlayerOverlap;
    [SerializeField] private OverlapDataSO explostionOverlap;
    [SerializeField] private string dynamicLayer;

    [Range(0, 100)]
    public float acceleration;

    [Range(0.1f, 20)]
    public float maxSpeed;

    [SerializeField] private float lifeTime;
    [SerializeField] private float playerCheckTime;
    [SerializeField] private float _currentVelocity = 3f;
    [SerializeField] private float delayTime = 0.4f;

    [Space]
    [SerializeField] private SpriteRenderer overlapRender;

    public UnityEvent<float> OnVelocityChnged;
    public UnityEvent<float> Explosioned;

    private Vector3 _moveDir;
    private Rigidbody2D _rbCompo;
    private bool _isTween;
    private bool _isFindTarget;
    private float _lastCheckTime;


    public void TryContact(ContactInfo info) => OnExplosion();
    private Collider2D ChackForTarget(OverlapDataSO data)
    {
        return Physics2D.OverlapCircle(transform.position, data.size, data.whatIsTarget);
    }

    private Sequence RotateTween(Vector3 dir, TweenCallback callback)
    {
        Vector3 r = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
        return DOTween.Sequence()
                .Append(renderCompo.transform.DOLocalRotate(r + new Vector3(0, 0, 360), delayTime, RotateMode.FastBeyond360))
                .AppendCallback(callback);

        //seq.Join(renderCompo.transform.DOLocalMove(-dir, speed * Time.fixedDeltaTime)
        //    .From(Vector3.zero)
        //    .SetSpeedBased()
        //    .SetLoops(2, LoopType.Yoyo))
        //    .SetEase(Ease.InOutCirc); 

    }
    private void OnCollisionSet()
    {
        colliderCompo.gameObject.layer = LayerMask.NameToLayer(dynamicLayer);
    }

    public void OnExplosion()
    {
        DOVirtual.DelayedCall(0.05f, () =>
        {
            SoundManager.Instance.PlaySound("Boom", transform.position.y);
            tween.Kill();
            var data = explostionOverlap;
            Explosioned?.Invoke(data.size);
            Destroy();

            var targets = Physics2D.OverlapCircleAll(transform.position, data.size, data.whatIsTarget);
            for (int i = 0; i < targets.Length; i++)
            {
                Collider2D target = targets[i];
                if (target.TryGetComponent<IExplosion>(out var explosion)) explosion.OnExplosion();
                else if (target.TryGetComponent<IBreakable>(out var blockable)) blockable.OnBreak();
                else if (target.TryGetComponent<IDamagable>(out var health)) health.TakeDamage(1);
            }
        }, false);
    }


    #region life cycile
    protected override void Awake()
    {
        base.Awake();
        _rbCompo = GetComponent<Rigidbody2D>();
        _lastCheckTime = Time.time;
        OnVelocityChnged?.Invoke(_currentVelocity);
    }
    private void Update()
    {
        if (_isFindTarget == false)
        {
            var time = Time.time;
            if (playerCheckTime < time - _lastCheckTime)
            {
                _lastCheckTime = time;
                FindTarget();
            }
        }
        else if (_isTween == false)
            _currentVelocity = CalculateSpeed(_moveDir);
    }

    private void FindTarget()
    {
        var target = ChackForTarget(chackPlayerOverlap);
        if (target)
        {
            SoundManager.Instance.PlaySound("bibik", transform.position.y);
            overlapRender.gameObject.SetActive(false);
            _isFindTarget = true;
            _isTween = true;
            OnCollisionSet();
            _moveDir = (target.transform.position - transform.position).normalized;
            tween = RotateTween(_moveDir, () =>
            {
                _isTween = false;
                tween = renderCompo.transform.DOShakePosition(2.5f, 0.3f, 25)
                .SetEase(Ease.OutExpo);
            });
            tween = DOVirtual.DelayedCall(lifeTime, () => Destroy(), false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnExplosion();
    }
    #endregion
    private float CalculateSpeed(Vector2 value)
    {
        if (value.sqrMagnitude > 0)
            _currentVelocity += acceleration * Time.deltaTime;
        OnVelocityChnged?.Invoke(_currentVelocity);

        return Mathf.Clamp(_currentVelocity, 0, maxSpeed);
    }

    private void Move()
    {
        _rbCompo.linearVelocity = _moveDir * _currentVelocity;
    }
    private void FixedUpdate()
    {
        if (_isTween == false)
        {
            Move();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chackPlayerOverlap.size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explostionOverlap.size);
    }
    private void OnValidate()
    {
        overlapRender.transform.localScale = transform.localScale * chackPlayerOverlap.size*2;
    }
}
