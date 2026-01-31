namespace CouponSystem.Application.DTOs;

public record CartItemDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}
