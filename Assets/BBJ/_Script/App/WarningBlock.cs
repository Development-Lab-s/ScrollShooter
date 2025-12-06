using DG.Tweening;
using UnityEngine;

public class WarningBlock : BlockBase, IBreakable, IExplosion
{
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float chackDistance;
    [SerializeField] private float speed;

    [SerializeField] private float explosionRadius;

    private Rigidbody2D _rbCompo;

    private Transform _target;
    private Vector3 _moveDir;

    private bool _isMove;

    public void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }
    public void TryBreak(ContactInfo info)
    {
        if (info.dashable.IsDash)
            OnBreak();
        else
            info.health.TakeDamage(1f);
    }
    public void OnBreak()
    {
        Destroy(gameObject);
        Debug.Log("파티클 생성");
    }

    private void RotateTween(Vector3 dir)
    {
        Sequence seq = DOTween.Sequence();
        Vector3 r = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
        seq.Append(renderCompo.transform.DOLocalRotate(r + new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360));
        seq.Join(renderCompo.transform.DOLocalMove(-dir, speed * Time.fixedDeltaTime)
            .From(Vector3.zero)
            .SetSpeedBased()
            .SetLoops(2, LoopType.Yoyo))
            .SetEase(Ease.InOutCirc);
        seq.AppendCallback(() => this._moveDir = dir);
    }
    private void Update()
    {
        if (_isMove == false)
        {
            if (ChackForTarget(chackDistance))
            {
                _isMove = true;
                RotateTween((_target.transform.position - transform.position).normalized);
            }
        }
    }
    private void FixedUpdate()
    {
        if (_isMove)
            Move();
    }
    private void Move()
    {
        _rbCompo.linearVelocity = _moveDir * speed;
    }
    private bool ChackForTarget(float distance)
    {
        var target = Physics2D.OverlapCircle(transform.position, distance, playerLayer);
        if(target != null) this._target = target.transform;
        return target != null;
    }
    private void OnTrigerEnter2D(Collider2D colition)
    {
        if(colition.TryGetComponent<IBreakable>(out var blockable))
        {
            OnExplosion();
        }
    }

    public void OnExplosion()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var item in targets)
        {
            if (item.TryGetComponent<IExplosion>(out var explosion))
            {
                explosion.OnExplosion();
                continue;
            }
            if (item.TryGetComponent<IBreakable>(out var blockable))
            {
                blockable.OnBreak();
                continue;
            }
            if (item.TryGetComponent<IDamagable>(out var health))
            {
                health.TakeDamage(1f);
            }
        }
    }
}
