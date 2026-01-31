using CouponSystem.Domain.Common;

namespace CouponSystem.Domain.Aggregates.Redemption;

public record ReservationToken : ValueObject
{
    public string Value { get; init; }
    
    public ReservationToken(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Reservation token cannot be empty");
            
        Value = value;
    }
    
    public static ReservationToken Generate()
    {
        return new ReservationToken($"res_{Guid.NewGuid():N}");
    }
    
    public static implicit operator string(ReservationToken token) => token.Value;
}
