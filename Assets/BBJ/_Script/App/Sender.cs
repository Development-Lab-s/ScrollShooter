using DG.Tweening;
using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Sender : MonoBehaviour
{
    [SerializeField] private ThrowBlock prefab;
    [SerializeField] private Transform target;
    [SerializeField] private AnimationHashDataSO hash;
    [SerializeField] private ChannelSO<Transform> channel;

    public UnityEvent<int, Action> AnimationTriggered;
    [SerializeField]private float throwDelayTime;

    [SerializeField] private Transform sendPos;
    private void Awake()
    {
        DOVirtual.DelayedCall(throwDelayTime,() => channel.InvolkEevnt(StartSend), false)
            .SetLoops(-1);
    }
    public void StartSend(Transform target)
    {
        var targetPos = target.transform.position;
        AnimationTriggered?.Invoke(hash, ()=>SendeFile(targetPos));
    }
    private void SendeFile(Vector2 target)
    {
        var b = PoolManager.Instance.PopByName(prefab.name) as  ThrowBlock;
        b.transform.position = transform.position;
        b.StartMove(sendPos.position, target);
    }
}
