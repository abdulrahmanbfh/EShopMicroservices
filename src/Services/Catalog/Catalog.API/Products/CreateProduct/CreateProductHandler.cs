namespace Catalog.API.Products.CreateProduct;


public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);


internal class CreateProductHandler (IDocumentSession session): ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create product entity
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        
        // Save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);


        // Return result
        return new CreateProductResult(Guid.NewGuid());

        
    }
}