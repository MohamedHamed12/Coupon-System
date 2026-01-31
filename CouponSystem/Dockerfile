FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/CouponSystem.API/CouponSystem.API.csproj", "src/CouponSystem.API/"]
COPY ["src/CouponSystem.Application/CouponSystem.Application.csproj", "src/CouponSystem.Application/"]
COPY ["src/CouponSystem.Domain/CouponSystem.Domain.csproj", "src/CouponSystem.Domain/"]
COPY ["src/CouponSystem.Infrastructure/CouponSystem.Infrastructure.csproj", "src/CouponSystem.Infrastructure/"]

RUN dotnet restore "src/CouponSystem.API/CouponSystem.API.csproj"

COPY . .
WORKDIR "/src/src/CouponSystem.API"
RUN dotnet build "CouponSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CouponSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CouponSystem.API.dll"]
