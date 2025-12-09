using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RecycleBin : BlockBase, IBreakable,IContactable
{

    [SerializeField] private float bounceTime;
    [SerializeField] private float bounceScale;

    [SerializeField] private float comebackTime;

    [SerializeField] private float delayTime;
    [SerializeField] private int warningBounceCnt;

    public UnityEvent Breaked;
    private Sequence _seq;
    public void OnBreak()
    {
        Breaked?.Invoke();
        Destroy();
    }

    public void TryContact(ContactInfo info)
    {
        OnBreak();
        if (info.player.IsDash == false && info.player.IsInvincible == false)
            info.player.TakeDamage(1f);
    }
    protected override void Awake()
    {
        Init();
    }
    public void Init()
    {
        _seq?.Kill();

        // 바운스 트윈
        _seq = DOTween.Sequence()
        .Append(transform.DOScale(bounceScale, bounceTime)).Join(renderCompo.SrCompo.DOColor(Color.red, bounceTime))
        .Append(transform.DOScale(1, comebackTime)).Join(renderCompo.SrCompo.DOColor(Color.white, comebackTime));

        float hafe = (delayTime / 2f)/ warningBounceCnt ;

        // 딜레이 트윈
        _seq.Append(DOTween.Sequence()
        .Append(renderCompo.transform.DOScale(1.2f, hafe)).Join(renderCompo.SrCompo.DOColor(Color.red, hafe)).Join(renderCompo.transform.DOShakePosition(hafe, 0.2f, 20))
        .Append(renderCompo.transform.DOScale(1f, hafe)).Join(renderCompo.SrCompo.DOColor(new Color(1f,0.5f,0.5f,1f), hafe))
        .SetDelay(delayTime / warningBounceCnt)
        .SetEase(Ease.OutQuad)
        .SetLoops(warningBounceCnt));

        _seq.SetLoops(-1);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale * bounceScale);
    }
}
