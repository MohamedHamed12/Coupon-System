# Coupon System - Enterprise E-commerce Solution

A production-ready coupon management system built with .NET 10, Domain-Driven Design (DDD), and Clean Architecture principles.

## Features

- ✅ Flexible coupon types (Percentage, Fixed Amount, BOGO, Free Shipping)
- ✅ Complex validation rules
- ✅ Two-phase commit for redemptions
- ✅ Optimistic concurrency control
- ✅ Soft delete support
- ✅ RESTful API with Minimal APIs
- ✅ PostgreSQL database
- ✅ Redis caching (ready to implement)
- ✅ RabbitMQ messaging (ready to implement)
- ✅ Docker support

## Architecture

```
CouponSystem/
├── src/
│   ├── CouponSystem.Domain/          # Core business logic
│   ├── CouponSystem.Application/     # Use cases & DTOs
│   ├── CouponSystem.Infrastructure/  # External integrations
│   └── CouponSystem.API/             # REST API endpoints
└── tests/
    └── CouponSystem.Tests/           # Unit & integration tests
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker & Docker Compose
- PostgreSQL 17 (or use Docker)

### Quick Start with Docker

```bash
# Start all services
docker-compose up -d

# Check services
docker-compose ps

# View logs
docker-compose logs -f api

# Stop services
docker-compose down
```

API will be available at: http://localhost:5000
Swagger UI: http://localhost:5000/swagger

### Local Development

```bash
# Restore dependencies
dotnet restore

# Apply migrations
cd src/CouponSystem.Infrastructure
dotnet ef database update --startup-project ../CouponSystem.API

# Run API
cd ../CouponSystem.API
dotnet run
```

## API Endpoints

### Create Coupon
```bash
POST /api/v1/coupons
Content-Type: application/json

{
  "code": "SAVE20",
  "discountType": "Percentage",
  "discountValue": 20.0,
  "startDate": "2024-01-01T00:00:00Z",
  "endDate": "2024-12-31T23:59:59Z",
  "maxTotalUses": 1000,
  "maxUsesPerUser": 1
}
```

### Redeem Coupon
```bash
POST /api/v1/coupons/redeem
Content-Type: application/json

{
  "couponCode": "SAVE20",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cartItems": [
    {
      "productId": "7b9e6f3a-2c5d-4e1b-9f8a-3d4c5e6f7a8b",
      "quantity": 2,
      "price": 49.99
    }
  ]
}
```

## Database Migrations

```bash
# Add migration
cd src/CouponSystem.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../CouponSystem.API

# Update database
dotnet ef database update --startup-project ../CouponSystem.API

# Remove last migration
dotnet ef migrations remove --startup-project ../CouponSystem.API
```

## Testing

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true
```

## Technology Stack

- **Framework**: .NET 10
- **Database**: PostgreSQL 17
- **ORM**: Entity Framework Core 8
- **Caching**: Redis
- **Messaging**: RabbitMQ
- **API**: Minimal APIs
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **Testing**: xUnit, FluentAssertions, Moq

## License

MIT License
