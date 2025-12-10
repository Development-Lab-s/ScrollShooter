using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChannelSO", menuName = "SO/Channel/Transform")]
public class ChannelSO : ChannelSO<Transform> {}

public class ChannelSO<T> : ScriptableObject
{
    public Recipiented<T> _subEvent;
    public void SubEevnt(Recipiented<T> subEvent)
    {
        _subEvent += subEvent;
    }
    public void UnsubEvent(Recipiented<T> unsubEvent)
    {
        _subEvent -= unsubEvent;
    }
    public void InvolkEevnt(Sended<T> collback)
    {
        _subEvent?.Invoke(collback);
    }
}
public delegate void Sended<T>(T value);
public delegate void Recipiented<T>(Sended<T> sended);
