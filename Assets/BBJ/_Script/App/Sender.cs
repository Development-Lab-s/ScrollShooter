using DG.Tweening;
using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

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
        DOVirtual.DelayedCall(throwDelayTime,() => channel.InvolkEevnt(StartSend))
            .SetLoops(-1);
    }
    public void StartSend(Transform target)
    {
        AnimationTriggered?.Invoke(hash, ()=>SendeFile(target));
    }
    private void SendeFile(Transform target)
    {
        var b = GameObject.Instantiate(prefab, transform.position, Quaternion.identity) as ThrowBlock;
        b.StartMove(sendPos.position, target.position);
    }
}
