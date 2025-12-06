using System;
using UnityEngine;

public interface IBreakable
{
    public void TryBreak(ContactInfo info);
    public void OnBreak();
}
public readonly struct ContactInfo
{
    public ContactInfo(IDamagable health = default, IDashable dashable = default)
    {
        this.dashable = dashable;
        this.health = health;
    }
    public readonly IDashable dashable;
    public readonly IDamagable health;
}
