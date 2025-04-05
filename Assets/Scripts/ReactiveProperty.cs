using System;

public class ReactiveProperty<T>
{
    public event Action<T> OnValueChange;

    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChange?.Invoke(value);
        }
    }

    public ReactiveProperty(T value)
    {
        Value = value;
    }

    public ReactiveProperty() : this(default) { }
}