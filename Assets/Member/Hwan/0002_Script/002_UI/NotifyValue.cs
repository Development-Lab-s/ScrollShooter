using System;

public interface IReadOnlyNotifyValue<T>
{
    public T Value { get; }

    public Action<T, T> OnValueCanged { get; }
}

public class NotifyValue<T> : IReadOnlyNotifyValue<T>
{
    public event Action<T, T> OnValueCanged;

    private T _value;
    Action<T, T> IReadOnlyNotifyValue<T>.OnValueCanged => OnValueCanged;

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
                OnValueCanged?.Invoke(before, _value);
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