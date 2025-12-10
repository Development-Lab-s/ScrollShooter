using DG.Tweening;
using UnityEngine;
using YGPacks.PoolManager;

public abstract class BlockBase : MonoBehaviour, IYgPoolable
{
    [SerializeField] protected BlockRenderer renderCompo;
    [SerializeField] protected Collider2D colliderCompo;
    public string Name => name;
    public GameObject GameObject => gameObject;
    protected Tween tween;

    protected virtual void Awake()
    {
        renderCompo = GetComponentInChildren<BlockRenderer>();
        colliderCompo = GetComponentInChildren<Collider2D>();
    }
    protected virtual void Destroy()
    {
        tween.Kill();
        Destroy(gameObject);
        //PoolManager.Instance.Push(this);
    }

    public virtual void AppearanceItem() { }
    public virtual void ResetItem() { }
}
