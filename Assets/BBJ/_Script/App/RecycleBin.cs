using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RecycleBin : BlockBase, IBreakable, IContactable
{
    [SerializeField] private OverlapDataSO overlapData;

    [Space]
    [SerializeField] private float defaultTweenDuration = 0.1f;
    [SerializeField] private float defaultTweenComebackDuration = 0.1f;
    [SerializeField] private float defaultTweenCoolTime = 0.5f;

    [Space]
    [SerializeField] private float bounceDelayTime = 0.2f;
    [SerializeField] private float bounceDuration = 0.1f;
    [SerializeField] private float bounceComebackDuration = 0.4f;
    [SerializeField] private float bounceCoolTime = 1f;

    private bool _isBounceCool;

    public UnityEvent Breaked;
    public void OnBreak()
    {
        Breaked?.Invoke();
        Destroy();
    }

    public void TryContact(ContactInfo info)
    {
        OnBreak();
        if (info.player.IsDash == false)
            info.player.TakeDamage(1);
    }
    private void Update()
    {
        if (_isBounceCool == false && CheckForTarget(overlapData))
        {
            _isBounceCool = true;
            tween?.Complete();
            tween = DOBounce(() =>
            {
                _isBounceCool = false;
                tween = DODefaultBounce()
                        .SetLoops(-1);
            });

        }
    }
    protected override void Awake()
    {
        base.Awake();
        tween = DODefaultBounce()
                .SetLoops(-1);
    }
    private Tween DODefaultBounce()
    {
        return DOTween.Sequence()
        .Append(renderCompo.transform.DOScale(1.1f, defaultTweenDuration)).Join(renderCompo.SrCompo.DOColor(Color.red, defaultTweenDuration)).Join(renderCompo.transform.DOShakePosition(defaultTweenDuration, 0.2f, 20))
        .Append(renderCompo.transform.DOScale(1f, defaultTweenComebackDuration)).Join(renderCompo.SrCompo.DOColor(new Color(1f, 0.5f, 0.5f, 1f), defaultTweenComebackDuration))
        .SetEase(Ease.OutQuad);
    }

    private Collider2D CheckForTarget(OverlapDataSO overlapData)
    {
        return Physics2D.OverlapBox(transform.position, transform.localScale, overlapData.whatIsTarget);
    }

    public Sequence DOBounce(TweenCallback tweenCalvack)
    {
        return DOTween.Sequence()
        .SetDelay(bounceDelayTime)
        .Append(transform.DOScale(overlapData.size, bounceDuration)).Join(renderCompo.SrCompo.DOColor(Color.red, bounceDuration))
        .Append(transform.DOScale(1, bounceComebackDuration)).Join(renderCompo.SrCompo.DOColor(Color.white, bounceComebackDuration))
        .SetDelay(bounceCoolTime)
        .AppendCallback(tweenCalvack);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale * overlapData.size);
    }
}
