using System;
using UnityEngine;

public interface IRecycle
{
    public Action<IRecycle> Destroyed { get; set; }
    public void Activate(Vector3 startPos);
}
