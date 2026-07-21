namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid value)
    {
        return value == Guid.Empty ? throw new DomainException("Order Item ID cannot be empty.") : new OrderItemId(value);
    }
}