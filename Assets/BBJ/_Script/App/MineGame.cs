using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MineGame : BlockBase, IContactable, IExplosion
{
    [SerializeField]private OverlapDataSO dataSO;
    public UnityEvent<float> Explosioned;

    public void OnExplosion()
    {
        DOVirtual.DelayedCall(0.05f, () =>
        {
            Destroy();
            Explosioned?.Invoke(dataSO.size);
            var targets = Physics2D.OverlapCircleAll(transform.position, dataSO.size, dataSO.whatIsTarget);
            for (int i = 0; i < targets.Length; i++)
            {
                Collider2D target = targets[i];
                if (target.TryGetComponent<IExplosion>(out var explosion)) explosion.OnExplosion();
                else if (target.TryGetComponent<IBreakable>(out var blockable)) blockable.OnBreak();
                else if (target.TryGetComponent<IDamagable>(out var damagable)) damagable.TakeDamage(1f);
            }
        });
    }

    public void TryContact(ContactInfo info)
    {
        OnExplosion();
        info.player.TakeDamage(1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dataSO.size);
    }
}
