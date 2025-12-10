using DG.Tweening;
using UnityEngine;

public class TestPlayerAAAA : MonoBehaviour, IPlayer, IDamagable
{
    public float Health { get; private set; }
    public bool IsInvincible => isInvincible;
    public bool isInvincible;
    public bool isDash;
    public bool IsDash => isDash;

    public void TakeDamage(int dmg)
    {
        dmg = Mathf.Max(0, dmg);
        Health -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IContactable block)) block.TryContact(new ContactInfo(this));
        if (collision.TryGetComponent(out IUseable useable)) useable.Use(new UseableInfo(this));
    }

    public void OnInvincible(float invincibleTime)
    {
        isInvincible = true;
        DOVirtual.DelayedCall(2, () => isInvincible = false, true);
    }
}
