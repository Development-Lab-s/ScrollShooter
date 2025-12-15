using System;
using UnityEngine;

public interface IBreakable: IContactable
{
    public void OnBreak();
}
public interface IContactable
{
    public void TryContact(ContactInfo info);
}
public readonly struct ContactInfo
{
    public ContactInfo(IPlayer player = default)
    {
        this.player = player;
    }
    public readonly IPlayer player;
}
