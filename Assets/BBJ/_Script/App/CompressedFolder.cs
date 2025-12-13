using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CompressedFolder : BlockBase, IBreakable, IContactable
{
    public UnityEvent Collitioned;
    [SerializeField] float knockbackPower;
    [SerializeField] float knockbackTime;
    public void OnBreak()
    {
        tween=BreakTween(() =>
        {
            Collitioned?.Invoke();
            Destroy(gameObject);
        });
    }

    private Sequence BreakTween(TweenCallback callback)
    {
        return DOTween.Sequence()
            .Append(transform.DOScale(2, 0.05f)
            .SetLoops(2, LoopType.Yoyo)
            .From(1))
            .AppendCallback(callback);
    }

    public void TryContact(ContactInfo info)
    {
        if (info.player.IsInvincible) OnBreak();
        Collitioned?.Invoke();
        info.player.OnKnockback(knockbackPower,knockbackTime);
    }
}
