using MediatR;

using CouponSystem.Application.DTOs;

namespace CouponSystem.Application.Commands;

public record RedeemCouponCommand : IRequest<RedemptionResultDto>
{
    public string CouponCode { get; init; } = null!;
    public Guid UserId { get; init; }
    public List<CartItemDto> CartItems { get; init; } = new();
}
