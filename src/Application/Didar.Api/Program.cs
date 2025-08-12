using Didar.Application.Interfaces;
using Didar.Application.Services;
using Didar.Infrastructure.Repositories;
using Didar.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddScoped<UserSubscriptionService>();
builder.Services.AddHttpClient<IPackagingService, PackagingServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PackagingService:BaseUrl"] ?? "http://localhost:5001");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
