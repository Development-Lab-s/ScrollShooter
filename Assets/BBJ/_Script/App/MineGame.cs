using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class MineGame : BlockBase, IBreakable, IExplosion
{
    [field: SerializeField]
    public GameObject BreakParticlePrefabs { get; private set; }

    [SerializeField]private float explosionRadius;


    public void OnBreak()
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

    public void OnExplosion()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius );
        foreach(var item in targets)
        {
            if (item.TryGetComponent<IExplosion>(out var explosion))
            {
                explosion.OnExplosion();
            }
            if(item.TryGetComponent<IBreakable>(out var blockable))
            {
                blockable.OnBreak();
            }
            if(item.TryGetComponent<IPlayer>(out var player))
            {
                Debug.Log("죽인다.");
            }
        }
    }

    public void TryBreak(ContactInfo info)
    {
        OnBreak();
        OnExplosion();
        Debug.Log("죽인다.");
    }
}
