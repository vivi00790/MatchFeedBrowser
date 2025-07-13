using ESportsMatchTracker.API.DbContexts;
using ESportsMatchTracker.API.Decorators;
using ESportsMatchTracker.API.MiddleWares;
using ESportsMatchTracker.API.Proxies;
using ESportsMatchTracker.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddHttpClient<IMatchService, MatchService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5105");
});

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IMatchService, MatchService>();
// Register the proxy for fetching matches from a dummy server and separate data fetching logic and data processing logic.
builder.Services.AddScoped<IDummyMatchProxy, DummyDummyMatchProxy>();
// Decorate the IMatchService with caching, instead of inject caching logic in MatchService directly.
builder.Services.Decorate<IMatchService, MatchServiceCache>();

// A demo for the scenario that different status matches are fetched from different servers.
builder.Services.AddHttpClient("DummyMatchClient:Scheduled", client =>
{
    client.BaseAddress = new Uri("http://localhost:5105");
});
builder.Services.AddHttpClient("DummyMatchClient:Live", client =>
{
    client.BaseAddress = new Uri("http://localhost:5105");
});
builder.Services.AddHttpClient("DummyMatchClient:Ended", client =>
{
    client.BaseAddress = new Uri("http://localhost:5105");
});

builder.Services.AddDbContext<LoggingDbContext>(options =>
    options.UseSqlite("Data Source=log.db"));

var app = builder.Build();
app.UseCors("AllowAll");
app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();
app.Run();
