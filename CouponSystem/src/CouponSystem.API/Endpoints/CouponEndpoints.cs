using MediatR;
using CouponSystem.Application.Commands;
using CouponSystem.Application.DTOs;

namespace CouponSystem.API.Endpoints;

public static class CouponEndpoints
{
    public static void MapCouponEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/coupons")
            .WithTags("Coupons")
            .WithOpenApi();
        
        group.MapPost("/", CreateCoupon)
            .WithName("CreateCoupon")
            .Produces<CouponDto>(StatusCodes.Status201Created);
        
        group.MapPost("/redeem", RedeemCoupon)
            .WithName("RedeemCoupon")
            .Produces<RedemptionResultDto>(StatusCodes.Status200OK);
    }
    
    private static async Task<IResult> CreateCoupon(
        CreateCouponCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/v1/coupons/{result.Id}", result);
    }
    
    private static async Task<IResult> RedeemCoupon(
        RedeemCouponCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}
