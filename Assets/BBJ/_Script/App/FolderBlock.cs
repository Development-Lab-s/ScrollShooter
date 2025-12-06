using DG.Tweening;
using UnityEngine;

public class FolderBlock : BlockBase, IBreakable
{
    [field: SerializeField]
    public GameObject BreakParticlePrefabs { get; private set; }

    public void OnBreak()
    {
        DoBreak();
    }

    public void TryBreak(ContactInfo info)
    {
        if (info.dashable.IsDash)
            OnBreak();
        else
            info.health.TakeDamage(1f);
    }

    private void DoBreak()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(2, 0.05f)
            .SetLoops(2, LoopType.Yoyo)
            .From(1));
        seq.AppendCallback(() =>
        {
            // 이펙트 소환
            var particl = Instantiate(BreakParticlePrefabs, transform.position, Quaternion.identity);
            // 파괴
            Destroy(gameObject);
            // +a 이벤트(공격, 텔포 등등)
        });
    }
}
