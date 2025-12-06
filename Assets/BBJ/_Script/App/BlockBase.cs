using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class BlockBase : MonoBehaviour
{
    [SerializeField] protected BlockRenderer renderCompo;

    public UnityEvent BlockBreaked;
    public Action<BlockBase> Destroyed;
}
