namespace CouponSystem.Application.DTOs;

public record RedemptionResultDto
{
    public string ReservationToken { get; init; } = null!;
    public decimal DiscountAmount { get; init; }
    public DateTime ExpiresAt { get; init; }
    public string Message { get; init; } = null!;
}
