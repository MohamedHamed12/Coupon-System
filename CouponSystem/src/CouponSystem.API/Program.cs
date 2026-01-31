using CouponSystem.Infrastructure.Persistence;
using CouponSystem.Infrastructure.Persistence.Repositories;
using CouponSystem.Domain.Repositories;
using CouponSystem.Application.Commands;
using CouponSystem.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<CouponDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Host=localhost;Database=coupon_db;Username=postgres;Password=postgres"));

// Repositories
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IRedemptionRepository, RedemptionRepository>();

// MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateCouponCommand).Assembly));

// AutoMapper
builder.Services.AddAutoMapper(typeof(CouponMappingProfile).Assembly);

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateCouponCommand>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

// Map endpoints
app.MapCouponEndpoints();

app.Run();
