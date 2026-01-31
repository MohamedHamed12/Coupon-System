using MediatR;
using CouponSystem.Application.DTOs;

namespace CouponSystem.Application.Commands;

public record CreateCouponCommand : IRequest<CouponDto>
{
    public string? Code { get; init; }
    public string DiscountType { get; init; } = null!;
    public decimal DiscountValue { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int? MaxTotalUses { get; init; }
    public int? MaxUsesPerUser { get; init; }
    public decimal MaxDiscountAmount { get; init; } = 999999.99m;
}
