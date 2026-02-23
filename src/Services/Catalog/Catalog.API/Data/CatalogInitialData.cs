namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if(await session.Query<Product>().AnyAsync(token: cancellation)) return;

        //MARTEN UPSERT will cater for existing records
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static List<Product> GetPreconfiguredProducts()
    {
        return
        [
            new Product
            {
                Id = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                Name = "IPhone X",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 950.00M,
                ImageFile = "product-1.png",
                Categories = ["Smart Phone"]
            },

            new Product
            {
                Id = new Guid("da2fd609-d754-4feb-8acd-c4aaea68e1ae"),
                Name = "Samsung 10",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 840.00M,
                ImageFile = "product-2.png",
                Categories = ["Smart Phone"]
            },

            new Product
            {
                Id = new Guid("2902c7ee-54b3-4225-9666-813df2a7580a"),
                Name = "Huawei Plus",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 650.00M,
                ImageFile = "product-3.png",
                Categories = ["Smart Phone"]
            },

            new Product
            {
                Id = new Guid("21c2d6bd-8154-4e89-9103-388c16adfa72"),
                Name = "Xiaomi Mi 9",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 470.00M,
                ImageFile = "product-4.png",
                Categories = ["Smart Phone"]
            },

            new Product
            {
                Id = new Guid("29950ed4-19a9-40ba-9284-78868c284d58"),
                Name = "HTC U11+ Plus",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 380.00M,
                ImageFile = "product-5.png",
                Categories = ["Smart Phone"]
            },

            new Product
            {
                Id = new Guid("d4964c22-17bd-44e6-8274-8a1ab5330aa5"),
                Name = "LG G7 ThinQ",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 240.00M,
                ImageFile = "product-6.png",
                Categories = ["Smart Phone"]
            }
        ];
    }
}