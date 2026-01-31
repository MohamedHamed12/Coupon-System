namespace CouponSystem.Application.DTOs;

public record CouponDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public string DiscountType { get; init; } = null!;
    public decimal DiscountValue { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string Status { get; init; } = null!;
    public bool IsActive { get; init; }
    public int CurrentUses { get; init; }
}
