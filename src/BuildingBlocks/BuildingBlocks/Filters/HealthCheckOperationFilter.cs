using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BuildingBlocks.Filters;

public class HealthCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if the current endpoint is the health check endpoint
        if (context.ApiDescription.RelativePath?.Contains("health", StringComparison.OrdinalIgnoreCase) != true) return;
        operation.Summary = "Health Check"; // You can add a custom summary
        operation.Description = "Checks the health of the application"; // Description for the health check
        operation.Responses = new OpenApiResponses
        {
            ["200"] = new OpenApiResponse
            {
                Description = "Healthy - The application is running properly"
            },
            ["503"] = new OpenApiResponse
            {
                Description = "Unhealthy - The application is not functioning correctly"
            }
        };
    }
}