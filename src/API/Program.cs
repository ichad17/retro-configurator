using Microsoft.EntityFrameworkCore;
using RetroConfigurator.Application.Interfaces;
using RetroConfigurator.Application.Services;
using RetroConfigurator.Domain.Repositories;
using RetroConfigurator.Infrastructure.Payment;
using RetroConfigurator.Infrastructure.Persistence;
using RetroConfigurator.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure Database
builder.Services.AddDbContext<RetroConfiguratorDbContext>(options =>
    options.UseInMemoryDatabase("RetroConfiguratorDb"));

// Register repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Register application services
builder.Services.AddScoped<IOrderService, OrderService>();

// Register payment service
builder.Services.AddScoped<IPaymentService>(sp =>
{
    var apiKey = builder.Configuration["Stripe:ApiKey"] ?? "sk_test_dummy_key";
    return new StripePaymentService(apiKey);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
