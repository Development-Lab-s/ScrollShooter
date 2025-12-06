using DG.Tweening;
using UnityEngine;

public class TestPlayerAAAA : MonoBehaviour, IPlayer, IDamagable
{
    public float Health { get; private set; }
    public bool isInvincible;
    public bool isDash;
    public bool IsDash => isDash;
    public bool IsInvincible => isDash;

    public void TakeDamage(float dmg)
    {
        dmg = Mathf.Max(0, dmg);
        Health -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IBreakable block))
        {
            if (IsInvincible)
                block.OnBreak();
            else
                block.TryBreak(new ContactInfo(this,this));
        }

        if (collision.TryGetComponent(out IUseable useable))
        {
            useable.Use(new UseableInfo(this));
        }
    }

    public void OnInvincible(float invincibleTime)
    {
        isInvincible = true;
        DOVirtual.DelayedCall(2, () => isInvincible = false, true);
    }
}
