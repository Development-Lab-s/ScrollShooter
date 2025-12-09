using DG.Tweening;
using UnityEngine.Events;

public class FolderBlock : BlockBase, IBreakable, IContactable
{
    public UnityEvent Breaked;
    public void OnBreak()
    {
        tween = DoBreak(()=> 
        {
            Breaked?.Invoke();
            Destroy();
        });
    }

    public void TryContact(ContactInfo info)
    {
        if (info.player.IsDash || info.player.IsInvincible)
            OnBreak();
        else
            info.player.TakeDamage(1f);
    }

    private Sequence DoBreak(TweenCallback callback)
    {
        return DOTween.Sequence()
            .Append(transform.DOScale(2, 0.05f)
            .SetLoops(2, LoopType.Yoyo)
            .From(1))
            .AppendCallback(callback);
    }
}
