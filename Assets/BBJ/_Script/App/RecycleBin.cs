using DG.Tweening;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecycleBin : BlockBase, IBreakable
{

    [SerializeField] private float bounceTime;
    [SerializeField] private float bounceScale;

    [SerializeField] private float comebackTime;

    [SerializeField] private float delayTime;
    [SerializeField] private int warningBounceCnt;

    private Sequence _seq;
    public void OnBreak()
    {

    }

    public void TryBreak(ContactInfo info)
    {
        if (info.dashable.IsDash)
            OnBreak();
        else
            info.health.TakeDamage(1f);
    }
    private void Awake()
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
}
