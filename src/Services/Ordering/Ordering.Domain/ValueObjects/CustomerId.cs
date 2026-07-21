namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid value)
    {
        return value == Guid.Empty ? throw new DomainException("Customer ID cannot be empty.") : new CustomerId(value);
    }
}