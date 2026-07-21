namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string? EmailAddress { get; } = null!;
    public string AddressLine { get; } = null!;
    public string State { get; } = null!;
    public string ZipCode { get; } = null!;
    public string Country { get; } = null!;

    protected Address()
    {
        
    }

    private Address(string firstName, string lastName, string? emailAddress, string addressLine, string state, string zipCode, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public static Address Of(string firstName, string lastName, string? emailAddress, string addressLine, string state, string zipCode, string country)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
        ArgumentException.ThrowIfNullOrWhiteSpace(state);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);

        return new Address(firstName, lastName, emailAddress, addressLine, state, zipCode, country);
    }
}