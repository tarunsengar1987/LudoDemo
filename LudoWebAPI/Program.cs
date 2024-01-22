using LudoWebAPI.Extensions;
using LudoWebAPI.Middleware;
using LudoWebAPI.Repositories.context;
using LudoWebAPI.Repositories.Context;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog to write logs to a file
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Add console logging (optional)
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day) // Add file logging
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog(); // Add Serilog as the logging provider
});

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IMongoDBContext, MongoDBContext>();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var contentRootPath = app.Environment.ContentRootPath;

// Configure static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(contentRootPath, "Avatars")),
    RequestPath = "/Avatars"
});

// Configure middleware
app.UseRouting();
app.UseHttpsRedirection();
app.UseErrorHandlingMiddleware(); // Use custom error handling middleware
app.UseCustomAuthorizationMiddleware(); // Use custom authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Configure endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<LudoChatHub>("/chathub");
    endpoints.MapControllers();
});

app.Run();
