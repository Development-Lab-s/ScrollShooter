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
        DOVirtual.DelayedCall(throwDelayTime,() => AnimationTriggered?.Invoke(hash,ThrowDeletFile))
            .SetLoops(-1);
    }
    private void ThrowDeletFile()
    {
        var a = Instantiate(prefab, transform.position, Quaternion.identity);
        a.StartMove(throwPos.position, transform.position + throwTargetPos);
    }
}
