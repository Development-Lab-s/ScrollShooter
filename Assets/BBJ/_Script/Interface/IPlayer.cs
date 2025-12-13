using System;
using UnityEngine;

public interface IPlayer : IDashable, IInvincible, IDamagable , IKnockbackable
{
    public CustomTween SetMaxSpeed(int nuwMaxSpeed, float duration, Action callback = default);
    //public void SetYSpeed(float speed, float duration, float originYSpeed);
}
public interface IDamagable
{
    public void TakeDamage(int dmg);
}
public interface IDashable
{
    //public void OnDash();
    public void OnDash();
    public bool IsDash { get;}
}
public interface IInvincible
{
    public bool IsInvincible { get; }
    public void OnInvincible(float invincibleTime);
}
public interface IKnockbackable
{
    public void OnKnockback(float knockPower, float knockTime);
}

