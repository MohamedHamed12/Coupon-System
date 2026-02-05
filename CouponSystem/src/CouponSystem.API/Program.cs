using CouponSystem.Infrastructure.Persistence;
using CouponSystem.Infrastructure.Persistence.Repositories;
using CouponSystem.Domain.Repositories;
using CouponSystem.Application.Commands;
using CouponSystem.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CouponSystem.API.Endpoints;

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

// Authentication & Authorization
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ReplaceThisWithASecureLongSecretKey";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "CouponSystem";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "CouponSystemUsers";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// User repository
builder.Services.AddScoped<CouponSystem.Domain.Repositories.IUserRepository, CouponSystem.Infrastructure.Persistence.Repositories.UserRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapAuthEndpoints();
app.MapCouponEndpoints();

app.Run();
