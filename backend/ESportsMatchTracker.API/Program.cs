using ESportsMatchTracker.API.Proxies;
using ESportsMatchTracker.API.Services;

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
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IDummyMatchProxy, DummyDummyMatchProxy>();

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

var app = builder.Build();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
