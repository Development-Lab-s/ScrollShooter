using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Thrower : MonoBehaviour
{
    [SerializeField] private ThrowBlock prefab;
    [SerializeField] private Vector3 throwTargetPos;
    public UnityEvent<int, Action> AnimationTriggered;
    [SerializeField] private AnimationHashDataSO hash;
    [SerializeField]private float throwDelayTime;

    [SerializeField] private Transform throwPos;
    private void Awake()
    {
        DOVirtual.DelayedCall(throwDelayTime,() => AnimationTriggered?.Invoke(hash,ThrowDeletFile), false)
            .SetLoops(-1);
    }
    private void ThrowDeletFile()
    {
        var b = PoolManager.Instance.PopByName(prefab.name) as ThrowBlock;
        b.transform.position = transform.position;
        b.StartMove(throwPos.position, transform.position + throwTargetPos);
    }
}
