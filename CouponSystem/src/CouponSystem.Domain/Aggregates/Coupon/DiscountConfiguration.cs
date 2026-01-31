using CouponSystem.Domain.Common;
using CouponSystem.Domain.Enums;

namespace CouponSystem.Domain.Aggregates.Coupon;

public record DiscountConfiguration : ValueObject
{
    public DiscountType Type { get; init; }
    public decimal Value { get; init; }
    
    public DiscountConfiguration(DiscountType type, decimal value)
    {
        if (type == DiscountType.Percentage && (value < 0 || value > 100))
            throw new ArgumentException("Percentage must be between 0 and 100");
            
        if (type == DiscountType.FixedAmount && value < 0)
            throw new ArgumentException("Fixed amount cannot be negative");
            
        Type = type;
        Value = value;
    }
    
    public decimal CalculateDiscount(decimal subtotal)
    {
        return Type switch
        {
            DiscountType.Percentage => subtotal * (Value / 100m),
            DiscountType.FixedAmount => Value,
            DiscountType.FreeShipping => 0m,
            DiscountType.BOGO => subtotal / 2m, // Simplified
            _ => throw new ArgumentException($"Unknown discount type: {Type}")
        };
    }
}
