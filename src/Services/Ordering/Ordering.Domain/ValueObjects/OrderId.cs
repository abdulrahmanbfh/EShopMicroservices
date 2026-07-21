namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid value)
    {
        return value == Guid.Empty ? throw new DomainException("Order ID cannot be empty.") : new OrderId(value);
    }
}