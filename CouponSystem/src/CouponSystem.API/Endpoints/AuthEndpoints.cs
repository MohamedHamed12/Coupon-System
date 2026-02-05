using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CouponSystem.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/auth")
            .WithTags("Auth");

        group.MapPost("/token", IssueToken)
            .WithName("IssueToken")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static IResult IssueToken(LoginRequest request, IConfiguration config)
    {
        // NOTE: Replace this with real credential validation
        if (request.Username != "test" || request.Password != "password")
            return Results.Unauthorized();

        var key = config["Jwt:Key"] ?? "ReplaceThisWithASecureLongSecretKey";
        var issuer = config["Jwt:Issuer"] ?? "CouponSystem";
        var audience = config["Jwt:Audience"] ?? "CouponSystemUsers";
        var expiresMinutes = int.TryParse(config["Jwt:ExpiresMinutes"], out var m) ? m : 60;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim(ClaimTypes.Name, request.Username),
            new Claim("role", "User")
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(new { token = tokenString, expires = token.ValidTo });
    }

    private record LoginRequest(string Username, string Password);
}
