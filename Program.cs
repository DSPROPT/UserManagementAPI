using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();

var app = builder.Build();

// In development, use the detailed developer exception page
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // If you prefer a custom controller-based global handler, uncomment:
//  app.UseExceptionHandler("/error");

    // Otherwise, we’ll rely on our custom ErrorHandlingMiddleware below.
}

// 1. Error-handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// 2. Authentication middleware
app.UseMiddleware<AuthenticationMiddleware>();

// 3. Logging middleware
app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
