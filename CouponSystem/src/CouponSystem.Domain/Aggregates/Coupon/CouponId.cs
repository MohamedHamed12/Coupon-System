namespace CouponSystem.Domain.Aggregates.Coupon;

public record CouponId(Guid Value)
{
    public static CouponId New() => new(Guid.NewGuid());
    public static CouponId From(Guid value) => new(value);
    public override string ToString() => Value.ToString();
}
