namespace Ordering.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("63dda53c-41e2-446d-aa5b-ec0341f67d32")), "Muhammad", "muhammad@example.com"),
        Customer.Create(CustomerId.Of(new Guid("5af9ade0-8581-47c9-a554-013da21a8de6")), "John", "john@example.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("f1c2e3d4-5678-90ab-cdef-1234567890ab")), "IPhone 15", 500),
        Product.Create(ProductId.Of(new Guid("a1b2c3d4-5678-90ab-cdef-1234567890ab")), "Samsung S24", 400),
        Product.Create(ProductId.Of(new Guid("92890f9f-e479-4ad9-a671-d4ca058b07bc")), "HUAWEI Pura", 650),
        Product.Create(ProductId.Of(new Guid("9617b9d7-ddf1-42d1-ab07-239de1e9631c")), "Honor Win", 450),
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Muhammad", "Ali", "muhammad@example.com", "123 Main St", "Khartoum", "12345", "Sudan");
            var address2 = Address.Of("John", "Doe", "john@example.com", "456 Oak Ave", "Khartoum", "67890", "Sudan");

            var payment1 = Payment.Of("Muhammad Ali", "1234 5678 9012 3456", "12/30", "321", 1);
            var payment2 = Payment.Of("John Doe", "9876 5432 1098 7654", "12/30", "123", 1);

            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("63dda53c-41e2-446d-aa5b-ec0341f67d32")),
                OrderName.Of("Order 1"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1);
            order1.Add(ProductId.Of(new Guid("f1c2e3d4-5678-90ab-cdef-1234567890ab")), 2, 500);
            order1.Add(ProductId.Of(new Guid("a1b2c3d4-5678-90ab-cdef-1234567890ab")), 1, 400);

            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("5af9ade0-8581-47c9-a554-013da21a8de6")),
                OrderName.Of("Order 2"),
                shippingAddress: address2,
                billingAddress: address2,
                payment2);
            order2.Add(ProductId.Of(new Guid("92890f9f-e479-4ad9-a671-d4ca058b07bc")), 1, 650);
            order2.Add(ProductId.Of(new Guid("9617b9d7-ddf1-42d1-ab07-239de1e9631c")), 2, 450);

            return new List<Order> { order1, order2 };
        }
    }
}