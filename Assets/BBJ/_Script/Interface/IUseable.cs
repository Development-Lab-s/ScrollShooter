using System;
using UnityEngine;

public interface IUseable
{
    public void Use(UseableInfo info);
}
public readonly struct UseableInfo
{
    public UseableInfo(IPlayer player)
    {
        this.Player = player;
    }
    public readonly IPlayer Player;
}
