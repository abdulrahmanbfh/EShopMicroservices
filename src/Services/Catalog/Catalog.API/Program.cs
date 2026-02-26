var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Health checks setup
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

// 1. Add services to the container.

// Registering API documentation and Swagger support
builder.Services.AddEndpointsApiExplorer(); // This is necessary for discovering endpoints for Swagger
builder.Services.AddSwaggerGen(options =>
{
    // Explicitly add health check endpoint to Swagger
    options.OperationFilter<HealthCheckOperationFilter>(); // Add a custom operation filter to include /health in Swagger
}); // Swagger setup

// Mediator pattern setup using MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly); // Register MediatR handlers and services from the assembly
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // Add custom behaviors like validation
    config.AddOpenBehavior(typeof(LoggingBehavior<,>)); // Add custom behaviors like logging
});

// FluentValidation setup
builder.Services.AddValidatorsFromAssembly(assembly); // Registers validators from the assembly

// Registering Carter for endpoint routing (similar to minimal APIs)
builder.Services.AddCarter(configurator: config =>
{
    config.RegisterEndpointsFromAssemblies([assembly]); // Register Carter endpoints from the assembly
});

// Marten setup for persistence
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!); // Register Marten with the database connection string
}).UseLightweightSessions(); // Use lightweight sessions for Marten
if (builder.Environment.IsDevelopment()) builder.Services.InitializeMartenWith<CatalogInitialData>(); // Initialize Marten with some initial data if in development environment

// Exception handling setup
builder.Services.AddExceptionHandler<CustomExceptionHandler>(); // Add custom exception handler

var app = builder.Build();

// 2. Configure the HTTP request pipeline.

// Global exception handler middleware setup
app.UseExceptionHandler(_ => { }); // Add a global exception handler (could be customized)

// 3. Swagger UI setup should be done after exception handling and health checks

if (app.Environment.IsDevelopment())
{
    // Swagger middleware is available only in Development environment
    app.UseSwagger(); // Enable Swagger generation
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"); // Point to the Swagger JSON
        options.RoutePrefix = string.Empty; // Makes Swagger UI the root page
    });
}

// Health check middleware should be placed first so it can be accessed at all times
//app.MapHealthChecks("/health", new HealthCheckOptions
//{
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//}).WithName("HealthCheck") // Gives it an explicit endpoint name
//.WithTags("Health")      // Groups it in Swagger
//.WithOpenApi();          // Pushes it to the ApiExplorer so Swagger sees it; // Add the health check endpoint

app.MapGet("/health", async (HealthCheckService healthCheckService) =>
    {
        // Run the registered health checks manually
        var report = await healthCheckService.CheckHealthAsync();

        // Return 200 OK if healthy, or 503 Service Unavailable if not
        return report.Status == HealthStatus.Healthy
            ? Results.Ok(report)
            : Results.Json(report, statusCode: StatusCodes.Status503ServiceUnavailable);
    })
    .WithName("HealthCheck")
    .WithTags("Health")
    .Produces<HealthReport>(StatusCodes.Status200OK)
    .Produces<HealthReport>(StatusCodes.Status503ServiceUnavailable);

// 4. Carter's endpoints (should be placed after health checks, exception handler, and Swagger)
app.MapCarter(); // Map the Carter endpoints

// 5. Run the application
app.Run(); // Run the app