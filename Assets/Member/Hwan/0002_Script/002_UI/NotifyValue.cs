using System;
using UnityEngine.Events;

public interface IReadOnlyNotifyValue<T>
{
    public T Value { get; }

    public Action<T, T> OnValueChanged { get; }
}

public class NotifyValue<T> : IReadOnlyNotifyValue<T>
{
    private T _value;
    public Action<T, T> OnValueChanged { get; set; }

    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            T before = _value;
            _value = value;
            if ((before == null && _value != null) || before.Equals(_value) == false)
                OnValueChanged?.Invoke(before, _value);
        }
    }


    public NotifyValue()
    {
        _value = default(T);
    }
    public NotifyValue(T value)
    {
        _value = value;
    }
}