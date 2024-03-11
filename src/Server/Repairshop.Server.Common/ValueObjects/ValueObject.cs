namespace Repairshop.Server.Common.ValueObjects;

public abstract class ValueObject<T>
{
    protected ValueObject(T value)
    {
        Value = value;
    }

    public T Value { get; set; }

    public static implicit operator T(ValueObject<T> valueObject) => valueObject.Value;
}
