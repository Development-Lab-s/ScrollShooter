using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class CompressedFolder : BlockBase, IBreakable
{
    public GameObject BreakParticlePrefabs { get; private set; }
    [SerializeField]private LayerMask whatIsPlayer;

    public void OnBreak()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(2, 0.05f)
            .SetLoops(2, LoopType.Yoyo)
            .From(1));
        seq.AppendCallback(() =>
        {
            // ÀÌÆåÆ® ¼ÒÈ¯
            var particl = Instantiate(BreakParticlePrefabs, transform.position, Quaternion.identity);
            // ÆÄ±«
            Destroy(gameObject);
        });
    }
    public void TryBreak(ContactInfo info)
    {
        Debug.Log("Æ¨°Ü³ª°¨");
    }
}
