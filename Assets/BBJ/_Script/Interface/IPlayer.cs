using UnityEngine;

public interface IPlayer : IDashable, IInvincible, IDamagable 
{
    //public void SetYSpeed(float speed, float duration, float originYSpeed);
}
public interface IDamagable
{
    public void TakeDamage(int dmg);
}
public interface IDashable
{
    //public void OnDash();
    public bool IsDash { get;}
}
public interface IInvincible
{
    public bool IsInvincible { get; }
    public void OnInvincible(float invincibleTime);
}

