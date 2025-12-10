using System;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Animator))]
public class BlockAnimater : MonoBehaviour
{
    private Animator _animCompo;
    private Action _callback;
    public void Awake()
    {
        _animCompo = GetComponent<Animator>();
    }
    public void SetAnimationFloat(int id, float value , Action callback = default)
    {
        _animCompo.SetFloat(id, value);
        this._callback += callback;
    }
    public void SetAnimationFloat(int id, float value)
    {
        _animCompo.SetFloat(id, value);
    }
    public void OnAnimationTrigger(int id)
    {
        _animCompo.SetTrigger(id);
    }
    public void OnAnimationTrigger(int id, Action callback = default)
    {
        _animCompo.SetTrigger(id);
        this._callback += callback;
    }
    private void OnAnimatorEnded()
    {
        _callback ?.Invoke();
        _callback = default;
    }
}
